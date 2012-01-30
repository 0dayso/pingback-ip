// 保险公司浮动广告
// var ADImgPath="test.gif" 
// var ADImgWidth=129 //这两行写图片的大小 
// var ADImgHeight=106 
var speed=2; 
var imageclick="#" //这里写点击图片连接到的地址 
var hideafter=0
var isie=0; 
if(window.navigator.appName=="Microsoft Internet Explorer"&&window.navigator.appVersion.substring(window.navigator.appVersion.indexOf("MSIE")+5,window.navigator.appVersion.indexOf("MSIE")+8)>=5.5) { 
isie=1; 
} 
else { 
isie=0; 
} 
if(isie){ 
var preloadit=new Image() 
preloadit.src=ADImgPath 
} 
function pop() { 
if(isie) { 
x=x+dx;y=y+dy; 
oPopup.show(x, y, ADImgWidth, ADImgHeight); 
if(x+ADImgWidth+5>screen.width) dx=-dx; 
if(y+ADImgHeight+5>screen.height) dy=-dy; 
if(x<0) dx=-dx; 
if(y<0) dy=-dy; 
startani=setTimeout("pop();",50); 
} 
} 
function dismisspopup(){ 
clearTimeout(startani) 
oPopup.hide() 
} 
function dowhat(){ 
if (imageclick=="dismiss") 
dismisspopup() 
else 
window.open(imageclick); 
} 
if(isie) { 
var x=0,y=0,dx=speed,dy=speed; 
var oPopup = window.createPopup(); 
var oPopupBody = oPopup.document.body; 
oPopupBody.style.cursor="hand" 
oPopupBody.innerHTML = '<IMG SRC="'+preloadit.src+'">'; 
oPopup.document.body.onmouseover=new Function("clearTimeout(startani)") 
oPopup.document.body.onmouseout=pop 
oPopup.document.body.onclick=dowhat 
pop(); 
if (hideafter>0) 
setTimeout("dismisspopup()",hideafter*1000) 
}