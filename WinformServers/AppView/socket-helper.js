var top = top || {};

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
    socket = new WebSocket(host);
    socket.onmessage = function (event) {
        if (typeof callGetMsg == "function")
            callGetMsg(event);
        if (typeof cllClose == "function") {
            cllClose(socket);
        }
    }
    socket.onopen =  function (event) {
        if (typeof callOpen == "function")
            callOpen(event);
        sendMsg += (sendMsg.indexOf("?") > 0 ? "&" : "?") + "isLong=true";
        socket.send(sendMsg);
    }
    return socket;
}