/**
 * 依赖项: 需引入jQuery文件,jquery版本在1.10以上
 * 作  者: jpw
 * 日  期: 2018-8-27 18:17:15
 * 描  述: 简单的数据模板引擎，只提供单层数据处理
 */

;
(function ($) {
    /**
     * 提供一组数据处理，传入json数据，检索需要处理的字符串，用值进行替换
     *      形如 <div id='{id}'>{id}</div>
     *      json [{id:"1"},{id:'2'}
     *      结果为<div id='1'>1</div><div id='2'>2</div>
     */
    $.extend({
       /**
         * 绑定数据到 块
         * @param {} json  后台json数据
         * @param {} selector document 的 id
         * @param {} isRplace 是否处理特殊字符 
         * @param {} replaceVale 要替换的值对象
         * @returns {} object
         */
        bandTempList:function(json, id, isRplace, replaceVale){
            return bandTempList(json, id, isRplace, replaceVale);
        },
        /**
         * 绑定数据到 块
         * @param {} json  后台json数据
         * @param {} selector jquery选择器
         * @param {} isRplace 是否处理特殊字符 
         * @param {} replaceVale 要替换的值对象
         * @returns {} object
         */
        bandTempJson:function(json, selector, isRplace, replaceVale){
            return bandTempJson(json, selector, isRplace, replaceVale);
        },
         /**
         * 处理数据 块
         * @param {} json  后台json数据
         * @param {} selector jquery选择器
         * @param {} isClear 是否替换多余符号
         * @param {} replaceVale 要替换的值对象
         * @returns {} object
         */
        stringFormat:function(json, cacheHtmls, isRplace, replaceVale){
            return stringFormat(json, cacheHtmls, isRplace, replaceVale);
        },
        /**
         * 处理数据 块
         * @param {} json  后台json数据
         * @param {} selector jquery选择器
         * @param {} isClear 是否替换多余符号
         * @returns {} object
         */
        stringFormatDesc:function(json, cacheHtmls, isClear, isSplit, replaceVale){
            return stringFormatDesc(json, cacheHtmls, isClear, isSplit, replaceVale);
        }
    });

    //缓存要操作模板
    var tempCache = {};
    /**
     * 绑定数据到 块 使用jQuery选择器
     * @param {} json  后台json数据
     * @param {} selector jquery选择器
     * @param {} isRplace 是否处理特殊字符 
     * @param {} replaceVale 要替换的值对象
     * @returns {} object
     */
    function bandTempJson(json, selector, isRplace, replaceVale) {
        isRplace = isRplace == undefined||isRplace ==true ? true : false;

        var data = $(selector);
        var cacheHtmls = tempCache[selector] = tempCache[selector] || data.html().replace(/[\r\t\n]/g, " ");
        var tbodyHtml = "";
        //  console.log(cacheHtmls);
        for (var i = 0; i < json.length; i++) {
            var innerHtmls = cacheHtmls;
            for (var _json in json[i]) {
                var value = json[i][_json];
                //value = value || '未知';
                (replaceVale != undefined) && (v = replaceVale[_json], value = v == undefined ? value : v[value]);
                var regex = new RegExp("[{]" + _json + "[}]", "g");
                innerHtmls = innerHtmls.replace(regex, value);
            }
            tbodyHtml += innerHtmls;
        }
        data.html(isRplace == true ? tbodyHtml.replace(/[{][a-zA-Z0-9]*[}]/g, "") :tbodyHtml);
        return tbodyHtml;
    }

    /**
     * 绑定数据到 块 根据 document 的Id
     * @param {} json  后台json数据
     * @param {} id 选择器替换模板的id
     * @param {} isRplace 是否处理特殊字符 
     * @param {} replaceVale 要替换的值对象
     * @returns {} object
     */
    function bandTempList(json, id, isRplace, replaceVale) {
        isRplace = isRplace == undefined||isRplace ==true ? true : false;

        var data = document.getElementById(id);
        var cacheHtmls = tempCache[id] = tempCache[id] || data.innerHTML.replace(/[\r\t\n]/g, " ");
        var tbodyHtml = "";
        //  console.log(cacheHtmls);
        for (var i = 0; i < json.length; i++) {
            var innerHtmls = cacheHtmls;
            for (var _json in json[i]) {
                var value = json[i][_json];
                //value = value || '未知';
                (replaceVale != undefined) && (v = replaceVale[_json], value = v == undefined ? value : v[value]);
                var regex = new RegExp("[{]" + _json + "[}]", "g");
                innerHtmls = innerHtmls.replace(regex, value);
            }
            tbodyHtml += innerHtmls;
        }
        data.html(isRplace == true ? tbodyHtml.replace(/[{][a-zA-Z0-9]*[}]/g, "") :tbodyHtml);
        return tbodyHtml;
    }


    /**
     * 处理数据 块
     * @param {} json  后台json数据
     * @param {} cacheHtmls 前台模板字符串
     * @param {} isClear 是否替换多余符号
     * @param {} replaceVale 要替换的值对象
     * @returns {} object
     */
    function stringFormat(json, cacheHtmls, isClear, isSplit, replaceVale) {
        isClear = isClear || true;
        var tbodyHtml = "";
        for (var i = 0; i < json.length; i++) {
            var innerHtmls = cacheHtmls;
            for (var _json in json[i]) {
                var value = json[i][_json];
                (replaceVale != undefined) && (v = replaceVale[_json], value = v == undefined ? value : v[value]);
                var regex = new RegExp("[{]" + _json + "[}]", "g");
                innerHtmls = innerHtmls.replace(regex, value);
            }
            tbodyHtml +=  innerHtmls;
            (isSplit == true || isSplit == undefined) && (tbodyHtml += i >= json.length - 1 ? "" : ",");
        }
        tbodyHtml=isClear == true ? tbodyHtml.replace(/[{][a-zA-Z0-9]*[}]/g, "") : tbodyHtml;
        return tbodyHtml;
    }




    /**
     * 处理数据 块
     * @param {} json  后台json数据
     * @param {} cacheHtmls 前台模板字符串
     * @param {} isClear 是否替换多余符号
     * @returns {} object
     */
    function stringFormatDesc(json, cacheHtmls, isClear, isSplit, replaceVale) {
        isClear = isClear || true;
        var tbodyHtml = "";
        for (var i = json.length - 1; i >= 0; i--) {
            var innerHtmls = cacheHtmls;
            for (var _json in json[i]) {
                var value = json[i][_json];
                (replaceVale != undefined) && (v = replaceVale[_json], value = v == undefined ? value : v[value]);

                var regex = new RegExp("[{]" + _json + "[}]", "g");
                innerHtmls = innerHtmls.replace(regex, value);
            }
            tbodyHtml +=  innerHtmls;
            (isSplit == true || isSplit == undefined) && (tbodyHtml += i >= json.length - 1 ? "" : ",");
        }
        tbodyHtml=isClear == true ? tbodyHtml.replace(/[{][a-zA-Z0-9]*[}]/g, "") : tbodyHtml;
        return tbodyHtml;
    }

})(window.jQuery);