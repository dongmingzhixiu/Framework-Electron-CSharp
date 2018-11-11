var top = top || {};



///**
// * 创建 socket 实例
// * @param {any} ip IP地址
// * @param {any} port 连接端口
// */
//function Socket(ip, port) {
//    ip = !ip ? "127.0.0.1" : ip;
//    port = !port ? "9909" : port;
//    top.ip = ip;
//    top.port = port;
//}
///**
// * 初始化创建连接实例，并连接
// **/
//top.initSK = function () {
//    //if (this.ip == top.ip && this.port == top.port && top.Socket != null) {
//    //    return Socket;
//    //}
//    var host = "ws://" + top.ip + ":" + top.port + "/"
//    top.socket = socket = new WebSocket(host);
//    return socket;
//}

///**
// * 连接服务器之后触发事件
// * @param callback 发送握手之后的 服务端响应 的 回调函数
// */
//top.openSK = function (callback) {
//    top.socket.onopen=function(event) {
//        if (typeof callback == "function") {
//            callback(event);
//        }
//    };
//}

///**
// * @param msg 要发送的消息
// * @param callback 在发送消息之后调用的 回调函数
// * 发送消息
// */
//top.sendCallFnSK = function (msg, callback) {
//    debugger;
//    top.socket.send = function (msg) {
//        if (typeof callback == "function") {
//            callback(event);
//        }
//    }
//}

//top.sendMsgSK = function (msg) {
//    top.socket.send(msg);
//}


///**
// * 接受客户端传来的消息
// * @param {any} callback
// */
//top.getMsgSK = function (callback) {
//    top.socket.onmessage=function (msg) {
//        if (typeof callback == "function") {
//            callback(msg);
//        }
//    };
//}

///**
// * 关闭连接
// * @param {any} callback
// */
//top.closeSK = function (callback) {
//    top.socket.onclose(function (event) {
//        if (typeof callback == "function") {
//            callback(event);
//        }
//    });
//}

/**
 * 创建 sock实例并发送消息 ，短连接 ，发送一条消息之后就关闭
 * @param {any} ip 发送内容
 * @param {any} port 发送内容
 * @param {any} sendMsg 发送内容
 * @param {any} callGetMsg 得到消息的回调函数
 * @param {any} callOpen 连接之后服务端发送回 消息 执行的回调函数
 * @param {any} cllClose 关闭连接的回调函数，默认自动关闭
 */
function initSocket(ip, port, sendMsg,callGetMsg,callOpen,cllClose) {
    top.ip = top.ip || ip;
    top.port = top.port || port;
    var host = "ws://" + top.ip + ":" + top.port + "/"
    socket = new WebSocket(host);
    socket.onmessage = function (event) {
        if (typeof callGetMsg == "function")
            callGetMsg(event);
        if (typeof cllClose == "function") {
                cllClose(socket);
            } else {
            socket.close();
        }
    }
    socket.onopen = function (event) {
        if (typeof callOpen == "function")
            callOpen(event);
        socket.send(sendMsg);
    }
    return socket;
}

/**
 * 创建 sock实例并发送消息 ，短连接 ，发送一条消息之后就关闭
 * @param {any} sendMsg 发送内容
 * @param {any} callGetMsg 得到消息的回调函数
 * @param {any} callOpen 连接之后服务端发送回 消息 执行的回调函数
 * @param {any} cllClose 关闭连接的回调函数，默认自动关闭
 */
function initSockets(sendMsg, callGetMsg, callOpen, cllClose) {
    var host = "ws://" + top.ip + ":" + top.port + "/"
    socket = new WebSocket(host);
    socket.onmessage = function (event) {
        if (typeof callGetMsg == "function")
            callGetMsg(event);
        if (typeof cllClose == "function") {
            cllClose(socket);
        } else {
            socket.close();
        }
    }
    socket.onopen = function (event) {
        if (typeof callOpen == "function")
            callOpen(event);
        socket.send(sendMsg);
    }
    return socket;
}


/**
 * 创建 sock实例并发送消息 ，长连接
 * @param {any} sendMsg 发送内容
 * @param {any} callGetMsg 得到消息的回调函数
 * @param {any} callOpen 连接之后服务端发送回 消息 执行的回调函数
 * @param {any} cllClose 关闭连接的回调函数，默认自动关闭
 */
function initSocketLong(sendMsg, callGetMsg, callOpen, cllClose) {
    var host = "ws://" + top.ip + ":" + top.port + "/"
    top.socket = top.socket = socket || new WebSocket(host);
    socket.onmessage = socket.onmessage|| function (event) {
        if (typeof callGetMsg == "function")
            callGetMsg(event);
        if (typeof cllClose == "function") {
            cllClose(socket);
        } else {
            top.socket.close();
        }
    }
    socket.onopen = socket.onopen || function (event) {
        if (typeof callOpen == "function")
            callOpen(event);
        top.socket.send(sendMsg);
    }
    return socket;
}