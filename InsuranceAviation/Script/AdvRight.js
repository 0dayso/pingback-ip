<!--
var smallrightshowad = true;
var smallrightToppx = 100;   //上端位置
var smallrightAdDivW = 155;  //宽度
var smallrightAdDivH = 233;  //高度
var smallrightPageWidth = 800; //页面多少宽度象素下正好不出现左右滚动条
var smallrightMinScreenW = 1024; //显示广告的最小屏幕宽度象素 

var contentURL = "ad01.gif";
var AdRightHtml = '<div align="center" style="color:green;font-size:23pt;font-family:黑体;"><iframe id="RightSmallAdvlr" name="showWnd" marginWidth="0" marginHeight="0" src="' + contentURL + '" frameBorder="no" width="90" scrolling="no" height="90"></iframe></div>';
var ClosebuttonHtml = '<div align="right" style="position: absolute;top:90px;right:0px;padding:4px;z-index:2000;width:90px;background-color:#f6f6f6"><a href="javascript:hidesmallrightad();" style="color:black;text-decoration:none;font-size:12px;">关闭</a></div>';
document.write ('<div id="smallRightDiv" style="position: absolute;background-color:white;z-index:1000;width:'+smallrightAdDivW+'px;height:'+smallrightAdDivH+'px;top:-1000px;word-break:break-all;display:none;">'+AdRightHtml+ClosebuttonHtml+'</div>');

function smallrightscall(){
 if(!smallrightshowad){return;}
 
 if (window.screen.width<smallrightMinScreenW){
  //alert("临时提示：\n\n显示器分辨率宽度小于"+MinScreenW+",不显示广告");
  smallrightshowad = false;  
  document.getElementById("smallRightDiv").style.display="none";
  return;
 } 
  
 var Borderpx = ((window.screen.width-smallrightPageWidth)/2-smallrightAdDivW)/2; 
 
 document.getElementById("smallRightDiv").style.display="";
 document.getElementById("smallRightDiv").style.top=document.body.scrollTop+smallrightToppx;
 document.getElementById("smallRightDiv").style.left=document.body.scrollLeft+document.body.clientWidth-document.getElementById("smallRightDiv").offsetWidth-Borderpx; 
 setTimeout("smallrightscall();",20);
  
}

function hidesmallrightad()
{
 smallrightshowad = false; 
 document.getElementById("smallRightDiv").style.display="none";
}
//window.onscroll=smallrightscall;
//window.onload=scall;
smallrightscall();
//-->
