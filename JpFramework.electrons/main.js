var electron=require("electron");
var {app,ipcMain,BrowserWindow} =electron;
app.on('window-all-closed', () => {
    app.quit()
})
app.on("ready", ()=>{
    myWork = new BrowserWindow({
        center:true,transparent:true,frame:false,titleBarStyle: 'hidden',minHeight:350,minWidth:550,width:500,height:300,
        webPreferences: {
            plugins: true
        }

    });

    myWork.loadURL(__dirname + "/app/htmls/login.html");
    myWork.show();
    // myWork.openDevTools();
} );



