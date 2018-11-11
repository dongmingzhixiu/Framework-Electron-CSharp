/***
 * 依赖文件 socket-helper.js
 * */

//配置 通信信息
function getSocketConf() {
    return {
        ip: '127.0.0.1',
        port: '9909'
    }
}

/**
 * 发送请求，返回后端处理的数据
 * @param {any} action action!method?par1=1&par2=2&parn=n&....
 * @param {any} callback 后端返回的回调函数
 */
function Hander(action, callback) {
    var con = getSocketConf();
    top.ip = !top.ip ? con["ip"] : top.ip;
    top.port = !top.port ? con["port"] : top.port;
    initSockets(action, callback);
}

/**
 * 发送请求，返回后端处理的数据
 * @param {any} action action!method?par1=1&par2=2&parn=n&....
 * @param {any} callback 后端返回的回调函数
 */
function Hander(action, callback) {
    var con = getSocketConf();
    top.ip = !top.ip ? con["ip"] : top.ip;
    top.port = !top.port ? con["port"] : top.port;
   return initSockets(action, callback, null, function (event) {});
}

/**
 * 发送请求，返回后端处理的数据 长链接 
 * @param {any} action action!method?par1=1&par2=2&parn=n&....
 * @param {any} callback 后端返回的回调函数
 */
function HanderLong(action, callback) {
    var con = getSocketConf();
    top.ip = !top.ip ? con["ip"] : top.ip;
    top.port = !top.port ? con["port"] : top.port;
    return initSocketLong(action, callback, null, function (event) { });
}