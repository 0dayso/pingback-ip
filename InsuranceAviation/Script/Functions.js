function addDate(type,NumDay,dtDate)
{
    var date = new Date(dtDate)
    type = parseInt(type) //类型 
    lIntval = parseInt(NumDay)//间隔

    switch(type)
      {
           case 6 ://年
          date.setYear(date.getYear() + lIntval)
          break;
         case 7 ://季度
          date.setMonth(date.getMonth() + (lIntval * 3) )
          break;
         case 5 ://月
          date.setMonth(date.getMonth() + lIntval)
          break;
         case 4 ://天
          date.setDate(date.getDate() + lIntval)
          break
         case 3 ://时
          date.setHours(date.getHours() + lIntval)
          break
         case 2 ://分
          date.setMinutes(date.getMinutes() + lIntval)
          break
         case 1 ://秒
          date.setSeconds(date.getSeconds() + lIntval)
          break;
         default:
      }

    var month = date.getMonth() + 1;
    var day = date.getDate();
    
//    if(month < 10)
//        month = "0" + month;

//    if(day < 10)
//        day = "0" + day;
    
    return date.getYear() +'-' + month + '-' + day;
  }
  
      function CovertCRLFToBR(s) 
    { 
     var i; 
     var s2 = s; 
     
     while(s2.indexOf("\r\n")>0) 
     { 
     i = s2.indexOf("\r\n"); 
     s2 = s2.substring(0, i) + "<br />" + s2.substring(i + 2, s2.length); 
     } 
     return s2; 
    } 