//'use strict';

const electron = require("electron");
const { app, ipcMain, BrowserWindow, session} = electron;


/**
 * 解决浏览器播放问题
 */
const path = require('path');
// 指定 flash 路径，假定它与 main.js 放在同一目录中。
let pluginName
switch (process.platform) {
    case 'win32':
        pluginName = 'pepflashplayer.dll'
        break
    case 'darwin':
        pluginName = 'PepperFlashPlayer.plugin'
        break
    case 'linux':
        pluginName = 'libpepflashplayer.so'
        break
}
app.commandLine.appendSwitch('ppapi-flash-path', path.join(__dirname, "/app/lib/" + pluginName))
app.commandLine.appendSwitch('ppapi-flash-version', '22.0.0.192');




app.on('new-window', (e) => {
    var url = e.url || e.originalEvent.url;
    if (url.indexOf('http:') >= 0 || url.indexOf('https:') >= 0) {
        if (typeof callback == "function") {
            callback(url);
        } else {
            shell.openExternal(url)
        }
    }
});

app.on('window-all-closed', () => {
    app.quit()
});

var myWork;
app.on("ready", (e) => {
    myWork = new BrowserWindow({
        center: true, transparent: true, frame: false, titleBarStyle: 'hidden', minHeight: 300, minWidth: 550, width: 500, height: 300,
        webPreferences: {
            plugins: true,
            allowDisplayingInsecureContent: true,
            allowRunningInsecureContent: true,
        }
    });

    myWork.loadURL(__dirname + "/app/htmls/login.html");
    myWork.show();

    //myWork.openDevTools();



    //session.defaultSession.webRequest.onBeforeSendHeaders(filter, (details, callback) => {
    //    e.parentDefault = true;
    //    details.requestHeaders['User-Agent'] = 'MyAgent'
    //    console.log(details.requestHeaders);
    //    callback({ cancel: false, requestHeaders: details.requestHeaders })
    //})
});

