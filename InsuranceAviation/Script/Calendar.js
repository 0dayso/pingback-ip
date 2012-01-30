String.prototype.Format = function(){
	var tmpStr = this;
	var iLen = arguments.length;
	for(var i=0;i<iLen;i++){
		tmpStr = tmpStr.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
	}
	return tmpStr;
}
Calendar = {
	//region Property
	today	 		:	new Date(),
	year			:	2005,
	month		:	8,
	date			:	21,
	curPosX		:	0,
	curPosY		:	0,
	curCapture	:	null,
	curDay		:	null,	
	//endregion

	//region Method
	display		:
		function(o, e, d){
			with(Calendar){
				o = typeof(o) == "object" ? o : document.getElementById(o);
				if(window.event){
					curPosX = document.body.scrollLeft + event.x;
					curPosY = document.body.scrollTop + event.y;
				} else{
					curPosX = e.pageX;
					curPosY = e.pageY;
				}
				if(o.value == "" && d) o.value = d;
				with(document.getElementById("Calendar__")){
					if(o != curCapture) {
						curCapture = o;
						if(style.display == "block"){
							style.left = curPosX + "px";
							style.top = curPosY + "px";
						}
						else load();
					}
					else{
						if (style.display == "block") style.display = "none";
						else load();
					} 
				}
			}
		},
	load			:
		function(){
			with(Calendar){
				curDay = loadDate(curCapture.value);
				with(curDay){
					year = getFullYear();
					month = getMonth() + 1;
					date = 	getDate();
				}
				init();
			}
		},
	init			:
		function(){
			with(Calendar){
				with(new Date(year, month-1, date)){
					year = getFullYear();
					month = getMonth() + 1;
					date = 	 getDate();
					setDate(1);
					var first = getDay();
					setMonth(getMonth()+1, 0)
					paint(first, getDate());
				}
			}
		},
	paint			:
		function(first, last){
			var calendar = document.getElementById("Calendar__");
			var grid = document.getElementById("dataGrid__");
			var i, l;
			l = Math.ceil((first + last)/7);
			if(!document.all){
				calendar.style.height = (41 + 19 * Math.ceil((first + last)/7)) + "px";
			}
			grid.innerHTML = new Array(l*7 + 1).join("<li><a></a></li>");
			with(Calendar){
				var strDate = "{0}-{1}".Format(year, month);
				var isTodayMonth = ((year == today.getFullYear()) && (month == today.getMonth() + 1));
				var isCurdayMonth = ((year == curDay.getFullYear()) && (month == curDay.getMonth() + 1));
				var todayDate = today.getDate();
				for(i=0;i<last;i++){
					grid.childNodes[first + i].innerHTML = '<a href="{2}-{1}"{0} onclick="Calendar.setValue({1});return false">{1}</a>'.Format(((i+1) == todayDate && isTodayMonth) ? ' class="today"' : isCurdayMonth && (i+1) == curDay.getDate()?' class="curDay"':'', i + 1, strDate);
				}
				document.getElementById("dateText__").innerHTML = '<a href="' + (year-1) + '年" onclick="Calendar.turn(-12);return false" title="上一年"><<</a> <a href="上一月" onclick="Calendar.turn(-1);return false" title="上一月"><</a> ' + year + " - " + month + ' <a href="下一月" onclick="Calendar.turn(1);return false" title="下一月">></a> <a href="' + (year+1) + '年" onclick="Calendar.turn(12);return false" title="下一年">>></a>';
				with(calendar){
					style.left = Calendar.curPosX + "px";
					style.top = Calendar.curPosY + "px";
					style.display = "block";
				}
			}
		},
	turn			:
		function(num){
			Calendar.month +=  num;
			Calendar.date = 1;
			Calendar.init();
		},
	setValue		:
		function(val){
			with(Calendar){
				curCapture.value = "{0}-{1}-{2}".Format(year, month, val);
				document.getElementById("Calendar__").style.display = "none";
			}
		},
	loadDate		:
		function(op, formatString){
			formatString = formatString || "ymd";
			var m, year, month, day;
			switch(formatString){
				case "ymd" :
					m = op.match(new RegExp("^((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})$"));
					if(m == null ) return new Date();
					day = m[6];
					month = m[5]*1;
					year =  (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10));
					break;
				case "dmy" :
					m = op.match(new RegExp("^(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))$"));
					if(m == null ) return new Date();
					day = m[1];
					month = m[3]*1;
					year = (m[5].length == 4) ? m[5] : GetFullYear(parseInt(m[6], 10));
					break;
				default :
					break;
			}
			if(!parseInt(month)) return new Date();
			month = month==0 ?12:month;
			var date = new Date(year, month-1, day);
			return (typeof(date) == "object" && year == date.getFullYear() && month == (date.getMonth()+1) && day == date.getDate())?date:new Date();
			
			function GetFullYear(y){return ((y<30 ? "20" : "19") + y)|0;}
			//function GetFull(m) {return ((m<10 ? "0" : "") + m);} 
		},
		toString : function(){return ["Calendar v1.0", "author:我佛山人", "email:wfsr@msn.com", "version:1.0"].join("\n");}
	//endregion
}
var	__calendar_html = "<style>";
		__calendar_html += "#Calendar__ {background-color:#eeeeee;width:157 !important;width:154px;position:absolute;display:none}";
		__calendar_html += "#Calendar__ ul{list-style-type:none;margin-left:-38px !important;margin:0 0 0 -30px;}";
		__calendar_html += "#Calendar__ ul li{display:block;width:20px;margin:1px;background-color:#fff;text-align:center;float:left;font:11px Tahoma}";
		__calendar_html += "#Calendar__ ul li a{height:18px;display:block;background-color:#fff;line-height:18px;text-decoration:none;color:#333}";
		__calendar_html += "#Calendar__ ul li a:hover{background:#336699;color:#FFF}";
		__calendar_html += "#Calendar__ #dateText__{font:12px Tahoma;text-align:center}";
		__calendar_html += "#Calendar__ #dateText__ a{font:10px Tahoma;text-decoration:none}";
		__calendar_html += "#Calendar__ #head__ li a{font:bold 12px Tahoma}";
		__calendar_html += "#Calendar__ #dataGrid__{}";
		__calendar_html += "#Calendar__ #dataGrid__ li a:hover{background:#dedede url(/plus/calendar/check.gif) right bottom no-repeat;color:red}";
		__calendar_html += "#Calendar__ #dataGrid__ .today{background:url(/plus/calendar/today.gif) center no-repeat;color:blue;}";
		__calendar_html += "#Calendar__ #dataGrid__ .curDay{background:#dedede url(/plus/calendar/check.gif) right bottom no-repeat;color:blue;}";
		__calendar_html += "</style>";
		__calendar_html += "<div id=\"Calendar__\">";
		__calendar_html += "<div id=\"dateText__\"></div>";
		__calendar_html += "<ul id=\"head__\" onclick=\"return false\">";
		__calendar_html += "<li><a href=\"#\">日</a></li><li><a href=\"#\">一</a></li><li><a href=\"#\">二</a></li><li><a href=\"#\">三</a></li><li><a href=\"#\">四</a></li><li><a href=\"#\">五</a></li><li><a href=\"#\">六</a></li>";
		__calendar_html += "</ul>";
		__calendar_html += "<ul id=\"dataGrid__\"></ul>";
		__calendar_html += "</div>";
document.write(__calendar_html);