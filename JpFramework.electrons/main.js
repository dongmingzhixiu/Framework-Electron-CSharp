
var {app,ipcMain,BrowserWindow} =require("electron");


app.on("ready", ()=>{
    win = new BrowserWindow({
        center:true,   minWidth: 1100, minHeight: 560,  backgroundColor: "black",
        webPreferences: {
            plugins: true
        }
    });
    win.loadURL(__dirname + "/app/index.html");
    win.show();
} );



