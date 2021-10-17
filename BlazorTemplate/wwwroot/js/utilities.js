

function initializeInactivityTimer(dotnetHelper) {
    console.log('start auto Timer');
    var timer;
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function resetTimer() {
        clearTimeout(timer);
        timer = setTimeout(logout, 360000);
    }

    function logout() {
        dotnetHelper.invokeMethodAsync("LogOut");
    }

}