window.themeStorage = {
    saveTheme: function (isDark) {
        localStorage.setItem("isDarkMode", isDark);
    },
    getTheme: function () {
        return localStorage.getItem("isDarkMode") === "true";
    }
};
