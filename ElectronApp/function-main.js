/**
 * 得到地址栏参数
 * @param {Object} key 
*/
exports.requestObj = function (url) {
    if (url == undefined || url == null || url == "") {
        return {};
    }
    console.log(url);
    var _request = "{'" + url.replace(/&/g, "','").replace(/=/g, "':'") + "'}";
    _request = eval("(" + _request + ")");
    return  _request;
}
exports.requestValue = function (url, key) {
    _request = exports.requestObj(url);
    return key == undefined ? _request : _request[key];
}

exports.uuid = function (len) {
    if (!len) {
        len = 16;
    }
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * len | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
