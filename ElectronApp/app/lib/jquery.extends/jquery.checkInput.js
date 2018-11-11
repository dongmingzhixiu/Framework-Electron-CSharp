/**
 * 依赖项: 需引入jQuery文件,jquery版本在1.10以上(layui.js要求jquery版本在1.10.0以上)		 (需要手动引用，且必须在当前js插件之前)
 * 		  和引入checkstyle.css文件 (默认已经引入，需要注意引入路径问题)
 * 		  和引入layui.js文件       (默认已经引入，需要注意引入路径问题)
 * 作  者: jpw
 * 日  期: 2018-8-27 18:17:15
 * 描  述: 校验用户输入信息
 */

;
//闭包限定命名空间
(function ($) {
    //扩展jquery 方法
    $.fn.extend({
        /**
         * 根据定义的校验信息进行校验信息
         * @param optionArrays:   需要验证的参数配置，详情参考下文defaults对象， 形如
                                                    [{
                                                            selector:"#name",
                                                            isEmpty:false,
                                                            checkType:["notNull","isChinese"],
                                                            message:"(必填)请输入用户名称，名称只能为汉字"
                                                            checkBefore:function(obj,opt,flg){
                                                                console.log("这是调用之前执行的方法");
                                                                alert("这是调用之前执行的方法");
                                                                console.log(obj,opt,flg);
                                                            },
                                                            checkAfter:function(obj,opt,flg){
                                                                console.log("这是调用之后执行的方法");
                                                                console.log(obj,opt,flg);
                                                                alert("这是调用之后执行的方法");
                                                            },
                                                            cheCallback:function(obj,opt,flg){
                                                                console.log("这是回调函数的方法");
                                                                console.log(obj,opt,flg);
                                                                alert("这是回调函数的方法");
                                                            }
                                                        },
                                                        {....},
                                                         ....
                                                    ]

         
         * @param  resultsFn:      验证操作完成时执行的回调函数。回调函数返回参数： (验证元素,验证参数,验证结果)
         * @param  initFileArray:  初始化文件的数组配置  形如：[{path:"./style.css",type:'css'},{path:"https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js",type:'js'}]
         */

        _initCheck: function (optionArrays, resultsFn, initFileArray) {
        	
        	var fns=resultsFn;
        	//判断传入参数类型
        	(typeof optionArrays=="function")&&(resultsFn=optionArrays);
        	(typeof fns=="object")&&(initFileArray=fns);
        	/**
        	 * 判断第一个参数传入的是配置数组还是函数，
        	 * 如果是函数，则采取行内属性的校验方式，
        	 * 如果是校验参数数组，则采取校验参数的方式校验
        	 */
        	(typeof optionArrays=="function"||typeof optionArrays=="undefined")&&(
                //保存创建校验的成员
                checkRangeSelector=$(this),
                optionArrays=$.fn._getInpCheOpts()
            ) ;
           

//            checkRangeSelector=$(this),
//            optionArrays = $.fn._getInpCheOpts();
        	
            //初始化需要的文件
            $._initExtendFile(initFileArray || []);

            $.each(optionArrays, function (index, options) {
                $.fn._addInputCheck(options);

            });

        },
        /**
         * 得到创建该类的成员下所有需要校验的参数配置
         */
        _getInpCheOpts:function(selectors){
            var find=["input:text","input:password","textarea","select","radio","checkbox"]
            var DOMArray=[];
            for(var i=0;i<find.length;i++){
            	var findDOM=$(checkRangeSelector).find(find[i]);
            	if(findDOM.length>0){
        			for(var f=0;f<findDOM.length;f++){
        				DOMArray.push($.fn._getInpCheOpt(findDOM[f]))
        			}
            	}
            }
            return DOMArray;
        },

        /**
         * 得到校验参数
         */
        _getInpCheOpt:function(DOM){
        	var options = defaults;
        	var _isEmpty=function(values){
        		if(values==undefined||values==null||values==""){
        			return true;
        		}else{
        			return false;
        		}
        	}
        	var _getAttr=function(attrName,docm){
	        	(docm==undefined)&&(docm=DOM);	
	        	return docm.getAttribute(attrName);
        	}
        	var opt={
        			"selector":function(){
                        var id=_getAttr("id");
        				if(_isEmpty(id)){
        					var _name=_getAttr("name");
        					if(_isEmpty(_name)){
        						var clas=_getAttr("class");
        						if(!_isEmpty(clas)){
	        						clas=clas.substring(0,clas.indexOf(" "));
	        						return "."+clas;
	        					}
        						throw "未定义name、id、class属性，请定义，或者采取另一种配置校验方式！";
        					}
        					return "[name='"+_name+"']";
        					
        				}
        				return '#'+id;
        				
        			},
        			"message":function(){
        				var message=_getAttr("message")||_getAttr("msg");
        				return _isEmpty(message)?"输入非法!":message;
        			},
        			"checkType":function(){
        				var checkType=_getAttr("checkType")||_getAttr("ctype");
        				if(_isEmpty(checkType)){
        					return [];
        				}
        				if(checkType.indexOf("[")>=0&&checkType.indexOf("]")>0){
        					checkType=typeof checkType=="string"?JSON.parse(checkType):[];
        				}else if(checkType.indexOf(",")>0){
        					checkType=checkType.split(",");
        				}
        				else if(checkType.length>0){
        					checkType=[checkType];
        				}
        				return checkType;
        			},
        			"showType":function(){
        				var showType=_getAttr("showType")||_getAttr("stype");
        				if(_isEmpty(showType)){
        					return 2;
        				}
        				return showType;
        			},
        			"minLength":function(){
        				var minLength=_getAttr("minLength")||_getAttr("minL");
        				if(_isEmpty(minLength)){
        					return 0;
        				}
        				return parseInt(minLength);
        			},
        			"maxLength":function(){
        				var maxLength=_getAttr("maxLength")||_getAttr("maxL");
        				if(_isEmpty(maxLength)){
        					return s=this.checkType().join(','),s.indexOf("isInteger")>=0||s.indexOf("isDouble")>=0?undefined:18;
        				}
        				return parseInt(maxLength);
        			},
        			"isEmpty":function(){
        				var isEmpty=_getAttr("isEmpty")||_getAttr("isEmp");
        				if(_isEmpty(isEmpty)){
        					return true;
        				}
        				return isEmpty=="true"||isEmpty=="1";
        			},
        			"checkBefore":function(){
        				return _getAttr("checkBefore")||_getAttr("chB");
        			},
        			"checkAfter":function(){
        				return _getAttr("checkAfter")||_getAttr("chA");
        			},
        			"cheCallback":function(){
        				return _getAttr("cheCallback")||_getAttr("chCall");
        			},
        			"checkCss":function(){
        				return _getAttr("checkCss")||_getAttr("chCss");
        			}
        		}
        	var options={};
        	for(var key in opt){
        		options[key]=opt[key]();
        	}
        	return options;
        },
        /**
         * 配置并校验一个校验文本框或控件
         */
        _addInputCheck:function(options){
        	
        	if(options==undefined&&$(this)!=undefined){
        		options=$.fn._getInpCheOpt($(this)[0]);
        	}
            var _defaultOpts = $.extend({}, defaults, options); // 覆盖插件默认参数
                var _selector = _defaultOpts["selector"];

                var opts = chedefaultCache[_selector] = chedefaultCache[_selector] || _defaultOpts;
                var $dom = $(_selector);


                //注册光标进入事件
                // $dom.focus(function(){
                //     var val=$dom.val();
                // });

                //注册光标离开事件 光标离开进行数据验证
                $dom.blur(function () {
                    var flg = checkVal($dom);
                    var _selector = $dom["selector"];
                    var opts = chedefaultCache[_selector];
                    if (typeof resultsFn == "function") {
                        flg = resultsFn($dom, opts, flg)
                    }
                    return flg;
                });

                var cs = opts["checkCss"];
                (typeof cs == "string" && cs != undefined) && (cs = eval("(" + cs + ")"));
                (cs != undefined) && ($dom.css(cs));
        },
        /**
         * 保存校验
         * @param  resultsFn ($dom【元素对象】,opts【元素校验配置参数】,flg【返回结果】)   
         * 校验元素完成返回结果之前执行 【回调函数】
         */
        _saveChe: function (resultsFn) {
            var flg = true;
            for (var _selector in chedefaultCache) {
                var $dom = $(_selector);
                if ($dom == null || $dom == undefined) {
                    continue;
                }
                _flg = checkVal($dom);
                if (_flg == false) {
                    flg = false;

                    //出现错误是停止下一项校验
                    //return flg;
                }
                var opts = chedefaultCache[_selector];
                if (typeof resultsFn == "function") {
                    flg = resultsFn($dom, opts, flg)
                }
            }
            return flg;
        }
    });

    var checkRangeSelector;

    /**
     * 校验数据
     * @param {要校验的元素对象} $dom 
     */
    var checkVal = function ($dom) {
        //判断元素状态，如果隐藏则不验证
        if (!$dom.is(':visible') || $dom.is(':hidden')) {
            return true;
        }

        var _select = $dom["selector"];
        var _opts = chedefaultCache[_select];
        var before = _opts["checkBefore"];
        (typeof before == "function") && (before($dom, _opts, flg));

        var valList = $._getVal($dom);
        var val = valList[0];
        var msg = "";
        var flg = true;
        var _cheCall = _opts["cheCallback"];
        if ((_opts["isEmpty"] && val == "") || (typeof val == "object" && val.length <= 0 && _opts["isEmpty"])) {
            flg = true;
        } else {
            if ((!_opts["isEmpty"] && val == "") || (!_opts["isEmpty"] && typeof val == "object" && val.length <= 0)) {
                flg = false;
                msg = valList[1] == "select" ? "您还没有选择,请选择!"
                    : valList[1] == "radio" ? "请选择一项!"
                        : valList[1] == "checkbox" ? "请最少选中一个！"
                            : "必填项，不能为空！";
            } else {
                if (typeof val == "string") {
                    //校验长度
                    var cheType = _opts["checkType"];
                    max = _opts["maxLength"], min = _opts["minLength"];
                    var cheTypeStr = cheType.join(',');
                    if (cheTypeStr.indexOf("isInteger") >= 0 || cheTypeStr.indexOf("isDouble") >= 0) {
                        if (undefined == max && parseFloat(val) < min) {
                            flg = false;
                            msg = "输入数必须 >=" + min +"";
                        }else if (parseFloat(val) > max || parseFloat(val) < min) {
                            flg = false;
                            msg = "输入数介于" + min + "-" + max + "之间";
                        }
                    } else {
                    	(max==undefined)&&(max=18);
                        if (val.length > max || val.length < min) {
                            flg = false;
                            msg = "请输入 " + (min == max ? min : (min + "-" + max)) + " 位字符";
                        }
                    }
                    if (flg) {
                        for (var i = 0; i < cheType.length; i++) {
                            var fun = cheType[i];
                            if (_cheType[fun] != undefined) {
                                flg = _cheType[fun](val, function (_val, _flg) {
                                    (typeof _cheCall == "function") && (_flg = _cheCall($dom, _opts, _flg))
                                    return _flg;
                                });
                                if (flg == false) break;
                            } else {
                                console.log("没有找到验证规则中包含：" + fun);
                            }
                        }
                    }
                }
            }
        }



        //使用定义的样式显示错误信息
        if (flg) {
            $._cheSuccess($dom, _opts["showType"], flg, val, msg == "" ? _opts["message"] : msg);
        } else {
            $._cheError($dom, _opts["showType"], flg, val, msg == "" ? _opts["message"] : msg);
        }
        var after = _opts["checkAfter"];
        (typeof after == "function") && (after($dom, _opts, flg));

        return flg;
    }

    /**
     * 缓存校验的配置信息
     */
    var chedefaultCache = {};

    /**
     * 校验参数设置
     */
    var defaults = {

        /**
         * （必填）选择器 和 jquery选择器用法相同 ，支持 #id / .class / div div:eq(2)等 【字符串】
         * 
         */
        selector: "",

        /**
         * （必填）提示文字 【字符串】
         */
        message: "输入非法，请重新输入",

        /**
         * （推荐使用）校验规则一组校验规则 【数组】
         */
        checkType: [],

        /**
         * 显示方式  1,2,3,4 上右下左四个方向 【标识字符】
         */
        showType: 2,

        /**
         * 最大长度 默认为18 【数字类型】
         */
        maxLength: undefined,

        /**
         * 最小长度 默认为0 【数字类型】
         */
        minLength: 0,

        /**
         * 是否允许为空 true or false  【bool类型】
         */
        isEmpty: false,

        /**
         * 校验之前执行 的方法  【函数】
         */
        checkBefore: null,

        /**
         * 校验之后执行 的方法 【函数】
         */
        checkAfter: null,

        /**
         * 回调函数 ；在执行每个校验规则时执行  【函数】
         */
        cheCallback: null,

        /**
         * 定义需要校验元素的样式 ，在初始化时处理元素样式  【键值对象】
         */
        checkCss: {},


    };

   
    /**
     * 定义校验规则
     */
    var _cheType = {
        /**
         * 私有处理方法
         */
        __regexVal: function (regex, val, callback) {
            var flg = regex.test(val);
            if (typeof callback == "function") {
                flg = callback(val, flg);
            }
            return flg;
        },
        /**
         * 不为空
         * @param val 值
         * @param callback 回调函数
         */
        notNull: function (val, callback) {
            val = val.replace(/^\s/g, "").replace(/\s￥/g, "");
            var flg = val.length > 0;
            if (typeof callback == "function") {
                callback(val, flg);
            }
        },

        /**
         * 小数类型(1位小数) 0.1~999999999999999999.9
         * @param callback 回调函数
         */
        isDouble1: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,1}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小数类型(2位小数) 0.01~999999999999999999.99
         */
        isDouble2: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,2}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小数类型(3位小数) 0.001~999999999999999999.999
         */
        isDouble3: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,3}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小数类型(4位小数) 0.0001~999999999999999999.9999
         */
        isDouble4: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,4}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小数类型(5位小数) 0.00001~999999999999999999.99999
         */
        isDouble5: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,5}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小数类型(6位小数) 0.000001~999999999999999999.999999
         */
        isDouble6: function (val, callback) {
            regex = /^[0-9]{1,18}[.]?[0-9]{0,6}$/;
            return this.__regexVal(regex, val, callback);
        },

        /**
         *整数 1-18位
        */
        isInteger: function (val, callback) {
            regex = /^[0-9]{1,18}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         *整数 字符串
        */
        isIntChar: function (val, callback) {
            regex = /^[0-9]{1,18}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 大小写 1-18位 
         */
        isLetter: function (val, callback) {
            regex = /^[a-zA-Z]{1,18}$/;
            return this.__regexVal(regex, val, callback);
        },


        /**
         * 大写 1-18位 
         */
        isUpperLetter: function (val, callback) {
            regex = /^[A-Z]{1,18}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 小写 1-18位 
         */
        isLowerLetter: function (val, callback) {
            regex = /^[a-z]{1,18}$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         * 手机号
         *  联通现有号段是：130、131、132、155、156、186、185，其中3G专属号段是：186、185。还有无线上网卡专属号段：145。
            移动现有号段是：134、135、136、137、138、139、150、151、152、157、158、159、182、183、188、187。
            电信现有号段是：133、153、180、181、189。
        */
        isPhone: function (val, callback) {
            //130','131','132','133','134','135','136','137','138','139','145','150','151','152','153','155','156','157','158','159','180','181','182','183','185','186','187','188','189','170,6,7,8','1907'
            regex = /^((1[3,5,8][0-9])|(14[5,7])|(17[0,6,7,8])|(19[7]))\d{8}$/;
            regex1 = /^0(([1,2]\d)|([3-9]\d{2}))\d{7,8}$/;
            return this.__regexVal(regex, val, callback) || this.__regexVal(regex1, val, callback);
        },
        /**
         * 邮箱
         */
        isEmail: function (val, callback) {
            regex = /\w+[@]\w+(\.\w+){1,2}/;
            return this.__regexVal(regex, val, callback);
        },

        /**
         * 中文字符
         */
        isChinese: function (val, callback) {
            regex = /^[\u4e00-\u9fa5]+$/;
            return this.__regexVal(regex, val, callback);
        },
        /**
         *除特殊符号外的任意字符
         */
        isAnyChar: function (val, callback) {
            regex = /[\?\!\@\#\$\%\^\&\*\(\)\_\+\{\}\:\"\|\>\?\<-\=\[\]\;\'\\\/\.\,\`\~\！\@\#\￥\%\…\…\&\*\（\）\—\—\+\{\}\：\“\|\《\》\？\；\‘\、\，\。\、\【\】]/;
            return !this.__regexVal(regex, val, callback);
        },
        /**
        *除特殊符号外的任意字符
        */
        isText: function (val, callback) {
            regex = /[\\]/;
            return !this.__regexVal(regex, val, callback);
        },

        /**
         * 校验是否是日期
         * yyyy-MM-dd   |  yyyy.MM.dd  | yyyy/MM/dd
         */
        isDate: function (val, callback) {
            var _val = val.replace(/[:]|[//]|-|[.]/g, "-");
            regex = /^[1-9]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$/;
            return this.__regexVal(regex, _val, callback);
        },

        /**
       * 校验是否是时间
       * HH:mm   | HH:mm:ss  | HH:mm:ss:ff
       */
        isTime: function (val, callback) {
            regex1 = /^(20|21|22|23|[0-1]\d):[0-5]\d$/;
            regex2 = /^(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d$/;
            regex3 = /^(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d:\d{1,4}$/;
            return this.__regexVal(regex1, val, callback)
                || this.__regexVal(regex2, val, callback)
                || this.__regexVal(regex3, val, callback);
        },
        /**
         * 校验是否是时间日期 yyyy-MM-dd HH:mm:ss
         */
        isDateTime: function (val, callback) {
            var _val = val;
            var val1 = _val.substr(0, 10), val2 = _val.substr(11, _val.length - 11).replace(/\s/g, "");
            return this.isDate(regex, val1, callback) && this.isTime(regex, val2, callback);
        },
        /**
         *身份证号验证
         */
        isIDCard: function (val, callback) {
            var pass = true;
            var call = function (callback,pass) {
                if (typeof callback == "function") {
                    pass = callback(val, pass);
                }
            }
            //18位身份证需要验证最后一位校验位
            if (val.length != 18) {
                return call(callback,pass);
            }

            var city = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江 ", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北 ", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏 ", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外 " };
            //地址编码校验
            if (!city[val.substr(0, 2)]) {
                return call(callback,pass);
            }

            //格式校验
            if (!val || !/^\d{6}(18|19|20)?\d{2}(0[1-9]|1[12])(0[1-9]|[12]\d|3[01])\d{3}(\d|X|x)$/.test(val)) {
                return call(callback,pass);
            }
            val = val.split('');
            //∑(ai×Wi)(mod 11)
            //加权因子
            var factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
            //校验位
            var parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2];
            var sum = 0, ai = 0, wi = 0;
            for (var i = 0; i < 17; i++) {
                ai = val[i];
                wi = factor[i];
                sum += ai * wi;
            }
            var last = parity[sum % 11];
            if (parity[sum % 11] != val[17]) {
                return call(callback,pass);
            }
            return true;
        }

    };

    //缓存错误信息的html字符
    var errorMsgHtml = {}

    /**
     * 扩展jQuery对象本身,定义错误成功提示信息
     * 可从外部重写
     */
    $.extend({
        /**
         * 成功处理方法
         */
        _cheSuccess: function (obj, showType, flg, val, msg) {
            var _select = $(obj)["selector"].replace(/'/g, "\'").replace(/"/g, "\'");
            _select = _select == "" ? "[name='" + $(obj).attr("name") + "']" : _select;
            var tip = errorMsgHtml[_select];
            if (tip != null) {
                layer.close(tip)
            }
            $(obj)[0].style["border"] = "1px solid #ccc";
            $(obj)[0].style["backgroundColor"] = "white";
            return flg;
        },
        /**
        * 错误处理方法
        */
        _cheError: function (obj, showType, flg, val, msg) {

            //使用layui的 tips
            var _select = $(obj)["selector"].replace(/'/g, "\'").replace(/"/g, "\'");
            _select = _select == "" ? "[name='" + $(obj).attr("name") + "']" : _select;
            var tip = errorMsgHtml[_select];
            if (tip != null) {
                layer.close(tip)
            }
            //修改提示信息，如果为 radio checkbox 时，提示信息显示在父元素
            var tipSelector=$(obj)["selector"];
            if(obj.length>1){
            	var tipSelector="#parentError_"+tipSelector.replace(/\[|\]|"|'/g,"").replace(/[.]/g,"_").replace(/=/g,"_");
            	if($(tipSelector).length<=0){
            		$($(obj)["selector"]).parent().append($('<span id="'+tipSelector.substring(1)+'">&nbsp;</span>'));            		
            	}
            }
//            var tipSpan=$(obj)["selector"] + (obj.length > 1 ? ":eq(" + (obj.length - 1) + ")" : "");
//            var _tipSpan=obj.length > 1 ?$(tipSpan).parent():$(tipSpan);
            var tip = layer.tips(msg, tipSelector, { time: 0, tips: [showType, '#ff9800'], tipsMore: true ,
                success: function(lay, index){
                	$(lay["selector"]).css({"cursor":"pointer","user-select":"none"});
                	$(lay["selector"]).click(function(){
                		 layer.close(index);
                	});
                }}
             );
            errorMsgHtml[_select] = tip;
            $(obj)[0].style["border"] = "1px solid #ff9800";
            $(obj)[0].style["backgroundColor"] = "#f8f2e3";
            return flg;
        },
        _getErrorHander:function(key){
           
            return key==undefined?errorMsgHtml:errorMsgHtml[key];
        },
        /**
         * 初始化加载js,css文件
         *@param 形如 fileArray  [{path:"./style.css",type:'css'}]
         */
        _initExtendFile: function (fileArray) {
            var optFile = [
                { path: "../layui/css/layui.css", type: 'css' },
                { path: "../layui/layui.all.js", type: 'js' },
            ];
            if (fileArray != undefined && fileArray != null) {
                optFile = $.extend({}, optFile, fileArray);
            }
            $.each(optFile, function (index, item) {
                var type = item["type"];
                if (type == "js") {
                    $.getScript(item["path"]);
                } else {
                    $('head').append('<link rel="stylesheet" type="text/css" href="' + item["path"] + '">');
                }
            });

        },
        /**
         * 定义得到函数值得方法
         * @param {要获取数据的jQuery对象，非DOM对象} $obj
         */
        _getVal: function ($obj) {
            var type = $obj.prop("tagName") || $obj.type;
            type = type.toLocaleLowerCase();
            (type == "input") && (type = $obj.attr("type"));
            var val;
            switch (type) {
                case "radio":
                    var name = $obj.attr("name");
                    val = $("[name='" + name + "']:checked").val();
                    break;
                case "checkbox":
                    val = [];
                    var name = $obj.attr("name");
                    var nameDom = $("[name='" + name + "']:checked");
                    for (var i = 0; i < nameDom.length; i++) {
                        val.push(nameDom[i].value);
                    }
                    break;
                case "select":
                    val = "";
                    var text = $obj.find(":selected").text();
                    if (text.replace(/^\s/g, "").replace(/\s$/g, "").length <= 0 || text == "" || text.indexOf("请选择") >= 0) {
                        val = "";
                    } else {
                        val = $obj.val();
                    }
                    break;
                default:
                    val = $obj.val();
                    break;
            }
            (typeof val == "string") && (val = val.replace(/^\s/g, "").replace(/\s$/g, ""));
            (val == undefined) && (val = "");
            return [val, type];
        }
    });



})(window.jQuery);

/**
 * HTMl行内验证简写
 * message <=> msg
 * checkType <=> ctype
 * showType <=> stype
 * maxLength <=> maxL
 * minLength <=> minL
 * isEmpty <=> isEmp
 * checkBefore <=> checkB
 * checkAfter <=> checkA
 * cheCallback <=> chCall
 * checkCss <=> chCss
 */