using BlazorTemplate.api.Context;
using BlazorTemplate.api.TokenHelpers;
using EmailService;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public AccountsController(UserManager<User> userManager, IConfiguration configuration, ITokenService tokenService, IEmailSender emailSender,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _tokenService = tokenService;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var user = new User { UserName = userForRegistration.Email, Email = userForRegistration.Email };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {

                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = result.Errors.Select(e => e.Description)
                });
            }

            await _userManager.AddToRoleAsync(user, "Viewer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //  var message = new Message(new string[] { user.Email }, "Confirmation Token", $"SUse ${token} to confirm registration ", null);
            //  await _emailSender.SendEmailAsync(message);

            return Ok(new ApiResponse<string> { IsSucessFull = true, Message = $"Use {token} to confirm email", Payload = token });

        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> SendEmailConfirmationOtp(OtpDto otp)
        {
            var user = await _userManager.FindByEmailAsync(otp.Email);
            if (user == null)
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //  var message = new Message(new string[] { user.Email }, "Confirmation Token", $"SUse ${token} to confirm registration ", null);
            //  await _emailSender.SendEmailAsync(message);

            return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Otp Sent Sucessful", Payload = token });
        }

        [HttpPost("Send2FACode")]
        public async Task<IActionResult> Send2FACode(TwoFADto twoFADto)
        {
            var user = await _userManager.FindByEmailAsync(twoFADto.Email);
            if (user == null)
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

            var token =  await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            //  var message = new Message(new string[] { user.Email }, "Confirmation Token", $"SUse ${token} to confirm registration ", null);
            //  await _emailSender.SendEmailAsync(message);

            return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "2FA Code Sent Sucessful", Payload = token });
        }


        [HttpPost("Verify2FACode")]
        public async Task<IActionResult> Verify2FACode(Verify2FADto verify2FADto)
        {
            var user = await _userManager.FindByEmailAsync(verify2FADto.Email);
            if (user == null)
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email",verify2FADto.token);
            if (result)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "2FA Verification Sucessful", Payload = "" });
            }
            return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "2FA Verification Failed", Payload = "" });

            //  var message = new Message(new string[] { user.Email }, "Confirmation Token", $"SUse ${token} to confirm registration ", null);
            //  await _emailSender.SendEmailAsync(message);

        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null)
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

             var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Email Confirmation Sucessful", Payload = "" });
            }

            return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "Email Confirmation Failed", Payload = "" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

            var result = await _signInManager.PasswordSignInAsync(userForAuthentication.Email, userForAuthentication.Password, false, lockoutOnFailure: true);
            if (result.IsLockedOut)
            {
                return Ok(
                    new ApiResponse<string>
                    {
                        IsSucessFull = false,
                        Message = "You Account is Locked Out, Please reset your password",
                        Payload = ""
                    });
            }

           var isEmailConfirmed =    await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
            {
                return Ok(
                    new ApiResponse<string>
                    {
                        IsSucessFull = false,
                        Message = "Email is not confirmed, Please Confirm your email",
                        Payload = ""
                    });
            }

                var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = await _tokenService.GetClaims(user);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, RefreshToken = user.RefreshToken });

        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
                return Ok(new ApiResponse<bool> { IsSucessFull = false, Message = "User with email does not exist" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            var message = new Message(new string[] { user.Email }, "Reset password token", $"Use this token {token} to reset your password", null);
            //   await _emailSender.SendEmailAsync(message);

            return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Password Reset Token Sent Sucessfully To Email", Payload = token });
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Model state is not valid",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return Ok(new ApiResponse<string> { IsSucessFull = false, Message = "User does not exist" });

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
              
                return Ok(new ApiResponse<IEnumerable<string>>
                {
                    IsSucessFull = false,
                    Message = "Password Reset Failed",
                    Payload = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                });
            }

            return Ok(new ApiResponse<string> { IsSucessFull = true, Message = "Password Reset Sucessful" });
        }
    }
}
