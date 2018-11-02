var edge = require("electron-edge-js");
// 定义方法var 
HanderCSharp = edge.func({
    assemblyFile: '../JpFramework/bin/Debug/JpFramework.dll',       // assemblyFile为dll路径
    atypeName: 'JpFramework.Startup',                               // RockyNamespace为命名空间，Study为类名
    methodName: 'EventHander'                                       // StudyMath为方法名
});

function ajax(param,callback){
    //param为参数
    //参数格式和 java status2 一致； 
    //格式 action!method?par1=值1&par2=值2
    HanderCSharp (param, callback);
}


function getResult(result,callback){
    var res=eval("("+result.replace(/"/g,"'")+")");;   
    if(res.state=="success"&&res.result.ds.length>0&&typeof callback=="function"){
       callback(res.result.ds);
    } 
    return res;
}