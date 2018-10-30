var electron=require("electron");
var {app,ipcMain,BrowserWindow} =electron;

app.on("ready", ()=>{
    winW=electron.screen.getPrimaryDisplay().workAreaSize.width
    winH=electron.screen.getPrimaryDisplay().workAreaSize.height
    myWork = new BrowserWindow({
        center:true,transparent:true,frame:false,titleBarStyle: 'hidden',height:winH,width:winW,
        webPreferences: {
            plugins: true
        }

    });

    myWork.loadURL(__dirname + "/app/index.html");
    myWork.show();
    // myWork.maximize();

} );



