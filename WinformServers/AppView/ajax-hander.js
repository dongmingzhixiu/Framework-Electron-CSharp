/***
 * 依赖文件 socket-helper.js
 * */

//配置 通信信息
function getSocketConf() {
    return {
        ip: '127.0.0.1',
        port: '9910'
    }
}

/**
 * 发送请求，返回后端处理的数据
 * @param {string} action action!method?par1=1&par2=2&parn=n&....
 * @param {function(res)} callback 后端返回的回调函数
 * @param {bool} isObect 是否转换成 对象
 */
function HttpHander(action, callback, isObect) {
    debugger;
    isObect = !isObect ? false :true;
    var http = "http://" + getSocketConf()["ip"] + ":" + getSocketConf()["port"] + "/" + action;
    $.ajax({
        url: http,
        dataType: "text",
        success: function (res) {
            if (isObect) {
                res = eval('(' + res+')')
            }
            callback(res);
        }
    });

}
/**
 * 发送请求，返回后端处理的数据
 * @param {string} action action!method?par1=1&par2=2&parn=n&....
 * @param {function(res)} callback 后端返回的回调函数
 * @param {bool} isObect 是否转换成 对象
 */
function HanderHttp(action, callback, isObect) {
    HttpHander(action, callback, isObect)
}

