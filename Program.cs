using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

class Program
{
	private static string Bread( string paths ) 
	{
		string b = "<div class=\"bread\">\n" ; 
		b += "<span>現在位置：</span>\n" ; 
		b += "<ul>\n" ; 
		b += "<li>\n" ; 
		if( paths == string.Empty ) 
		{
			b += "首頁\n" ; 
			b += "</li>" ; 
		}
		else 
		{
			b += "<a href=\"\">首頁</a>\n" ; 
			b += "</li>\n" ; 
			if( paths.Contains( '>' ) ) 
			{
				foreach( var p in paths.Split( '>' )[..^1] ) 
				{
					b += "<li>\n" ; 
					b +=$"<a href=\"{ p.Split( ':' )[0] }\">\n" ; 
					b +=$"{ p.Split( ':' )[0] }\n" ; 
					b += "</a>\n" ; 
					b += "</li>\n" ; 
				}
				b += "<li>\n" ; 
				b +=$"{ paths.Split( '>' )[^1] }\n" ; 
				b += "</li>\n" ; 
			}
			else 
			{
				b += "<li>\n" ; 
				b +=$"{ paths }\n" ; 
				b += "</li>\n" ; 
			}
		}
		b += "</ul>\n" ; 
		b += "</div>\n" ; 
		return b ; 
	}
	private static string Head( string pageTitle ) 
	{
		return """
<!DOCTYPE html>
<html lang="zh-Hant-TW">
<head>
<meta charset="utf-8" />
<base href="static" />
<title>
""" + pageTitle + """
 - 臺中市立臺中第一高級中等學校學生自治聯合會法規資料庫</title>
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
<meta name="author" content="louischo303@" />
<meta name="keywords" content="臺中市立臺中第一高級中等學校,臺中市立臺中第一高級中等學校學生自治聯合會,臺中一中學聯會,台中一中學聯會,中一中學聯會,一中學聯會,中一中學生會,一中學生會,自治部,臺中一中,台中一中,中一中,一中,一中學聯會,學聯會,法規,法規資料庫,學聯會法規,學生會,學生議會,學生政黨,政黨,組織章程,章程,三權分立,一讀,二讀,第一讀會,第二讀會,組織及職權行使法,職權行使法,議會監察使,會長,學生會會長,學生會長,學生議會議長,議長,評議委員會,評議委員會主任委員,主任委員,評委會,評委會主委,咨詢委員,咨委" />
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Fira+Code&family=Noto+Sans+KR:wght@100..900&family=Noto+Serif+KR:wght@200..900&family=IBM+Plex+Mono:wght@500&family=Noto+Sans+TC:wght@100..900&family=Noto+Sans+JP:wght@100..900&family=Noto+Sans+SC:wght@100..900&family=Noto+Serif:ital,wght@0,100..900;1,100..900&family=Noto+Sans+Mono:wght@100..900&family=Noto+Sans+Display:ital,wght@0,100..900;1,100..900&family=Noto+Serif+JP:wght@200..900&family=Noto+Serif+SC:wght@200..900&family=Noto+Sans:wght@700&family=Noto+Serif+TC:wght@200..900&display=swap">
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/print.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/fonts.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/color.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/site.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/p.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/home.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/breadcrumb.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/app.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/law.css" />
<link rel="stylesheet" href="https://tcfshsu.github.io/law/css/darkmode.css" />
</head>
<body>
<style>
#lqc 
{
color : var( --header-bg ) ; 
background: linear-gradient(20deg, #fae0f7, #bd0f83);
border : 0 ; 
border-radius : 8.5px ; 
}
label[for="lqc"] 
{
display : none ; 
color : #fffad0 ; 
}
#lql , label[for="lql"] 
{
color : #ffc8d2 ; 
display : none ; 
}
#lql 
{
background: linear-gradient(20deg, #001d12, #a165fa);
border : 0 ; 
border-radius : 8.5px ; 
}
label[for="lql"] , label[for="lqc"] 
{
font-size : .8rem ; 
flex-shrink : 0 ; 
}
@media ( min-width : 768px ) 
{
#lql , label[for="lql"] , label[for="lqc"] 
{
display : inline-block ; 
}
}
</style>
<div>
<header>
<div class="top-guide">
<ul>
<li><a href="guide" tabindex="1">網站導覽</a></li>
<li id="zoom">
<span id="z_s" tabindex="1" style="font-size:13.6px;" onclick="document.documentElement.style.zoom='85%';this.className='Y';document.getElementById('z_m').className='';document.getElementById('z_l').className='';">A</span>
<span id="z_m" class="Y" tabindex="2" style="font-size:16px;" onclick="document.documentElement.style.zoom='100%';this.className='Y';document.getElementById('z_s').className='';document.getElementById('z_l').className='';">A</span>
<span id="z_l" tabindex="3" style="font-size:18.4px;" onclick="document.documentElement.style.zoom='115%';this.className='Y';document.getElementById('z_s').className='';document.getElementById('z_m').className='';">A</span>
</li>
</ul>
</div>
<div class="top">
<a tabindex="4" class="" href=""><img src="https://tcfshsu.github.io/law/i/icon/logo.png" alt="首頁" /></a>
<form action="laws" method="get">
<label for="lqc">類別：</label>
<select tabindex="5" name="c" id="lqc">
<option selected value="">不限</option>
<option value="c">中央法規</option>
<option value="ex">行政法規</option>
<option value="l">立法法規</option>
<option value="j">司法法規</option>
<option value="el">選舉法規</option>
</select>
<label for="lql">位階：</label>
<select tabindex="5" name="l" id="lql">
<option selected value="">不限</option>
<option value="章程">章程</option>
<option value="法律">法律</option>
<option value="命令">命令</option>
</select>
<p>
<input tabindex="6" type="search" placeholder="輸入關鍵字以搜尋" name="q" value="">
<input tabindex="7" type="submit" style="background-image:url(https://tcfshsu.github.io/law/i/icon/search_w.svg);width:24px;height:24px;background-color:#0000;border:0;cursor:pointer;" value="" alt="搜尋！" />
</p>
</form>
<div tabindex="8" onclick="const b=document.getElementsByTagName('nav')[0].className;document.getElementsByTagName('nav')[0].className=b?'':'collapse';if(b)this.getElementsByTagName('path')[0].setAttribute('d','M3 3L21 21M3 21L21 3');else this.getElementsByTagName('path')[0].setAttribute('d','M3 12L21 12M3 6L21 6M3 18L21 18');">
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M3 12L21 12M3 6L21 6M3 18L21 18" /></svg>
</div>
</div>
<nav class="collapse">
<ul>
<li><a href="laws?c=c" tabindex="9">中央法規</a></li>
<li><a href="laws?c=ex" tabindex="9">行政法規</a></li>
<li><a href="laws?c=l" tabindex="9">立法法規</a></li>
<li><a href="laws?c=j" tabindex="9">司法法規</a></li>
<li><a href="laws?c=el" tabindex="9">選舉法規</a></li>
<li><a href="cases" tabindex="9">判例查詢</a></li>
<li><a href="parties" tabindex="9">政黨查詢</a></li>
<li><a href="rel" tabindex="9">相關連結</a></li>
</ul>
</nav>
</header>
<div>
<div id="top"></div>
<main class="main">
""" ; 
	}
	static void Main(string[] args)
	{
		string json = File.ReadAllText( "./json/laws.json" ) ;
		string pjson = File.ReadAllText( "./json/parties.json" ) ;
		string cjson = File.ReadAllText( "./json/cases.json" ) ;
		LawRoot[]? lr = JsonSerializer.Deserialize<LawRoot[]>( json ) ;
		Py[]? pr = JsonSerializer.Deserialize<Py[]>( pjson ) ;
		Casess[]? cr = JsonSerializer.Deserialize<Casess[]>( cjson ) ;
		L[] laws = lr![0].Laws ; 
		P[] parties = pr![0].Parties ; 
		Case[] cases = cr![0].Cases ; 
		string latest_json = File.ReadAllText( "./json/latest.json" ) ;
		Latests[]? las = JsonSerializer.Deserialize<Latests[]>( latest_json ) ;
		// string now = "UTC" + DateTime.UtcNow.ToString( "s" ).Replace( "/" , "" ).Replace( ':' , '_' ) ; 
		// Directory.CreateDirectory($"./{ now }" ) ; 
		// Directory.CreateDirectory($"./{ now }/cases" ) ; 
		// Directory.CreateDirectory($"./{ now }/latest" ) ; 
		// Directory.CreateDirectory($"./{ now }/parties" ) ; 
		// File.Copy( "./html/index.html" , "./" + now + "/index.html" , true ) ;
		// File.Copy( "./html/guide.html" , "./" + now + "/guide.html" , true ) ;
		// File.Copy( "./html/latest.html" , "./" + now + "/latest.html" , true ) ;
		// File.Copy( "./html/print.html" , "./" + now + "/print.html" , true ) ;
		// File.Copy( "./html/open.html" , "./" + now + "/open.html" , true ) ;
		// File.Copy( "./html/rel.html" , "./" + now + "/rel.html" , true ) ;
		// File.Copy( "./html/s.html" , "./" + now + "/s.html" , true ) ;
		// File.Copy( "./html/parties.html" , "./" + now + "/parties.html" , true ) ;
		// File.Copy( "./html/parties/detail.html" , "./" + now + "/parties/detail.html" , true ) ;
		// File.Copy( "./html/cases.html" , "./" + now + "/cases.html" , true ) ;
		// File.Copy( "./html/cases/detail.html" , "./" + now + "/cases/detail.html" , true ) ;
		Directory.CreateDirectory("html");
		Directory.CreateDirectory("html/laws");
		Directory.CreateDirectory("html/cases");
		Directory.CreateDirectory("html/latest");
		Directory.CreateDirectory("html/parties");
		Directory.CreateDirectory("html/laws/law");
		Directory.CreateDirectory("html/laws/lawe");
		Directory.CreateDirectory("html/laws/law/c");
		Directory.CreateDirectory("html/laws/law/e");
		Directory.CreateDirectory("html/laws/law/x");
		Directory.CreateDirectory("html/laws/law/l");
		Directory.CreateDirectory("html/laws/law/j");
		Directory.CreateDirectory("html/laws/lawe/c");
		Directory.CreateDirectory("html/laws/lawe/e");
		Directory.CreateDirectory("html/laws/lawe/x");
		Directory.CreateDirectory("html/laws/lawe/l");
		Directory.CreateDirectory("html/laws/lawe/j");
		Directory.CreateDirectory("html/cases/detail");
		Directory.CreateDirectory("html/latest/detail");
		Directory.CreateDirectory("html/parties/detail");
		File.Delete( "./html/index.html" ) ; 
		File.Delete( "./html/guide.html" ) ; 
		File.Delete( "./html/latest.html" ) ; 
		File.Delete( "./html/print.html" ) ; 
		File.Delete( "./html/open.html" ) ; 
		File.Delete( "./html/rel.html" ) ; 
		File.Delete( "./html/s.html" ) ; 
		File.Delete( "./html/parties.html" ) ; 
		File.Delete( "./html/parties/detail.html" ) ; 
		File.Delete( "./html/cases.html" ) ; 
		File.Delete( "./html/cases/detail.html" ) ; 
		string lty , ltm , ltd , pty , ptm , ptd , cty , ctm , ctd ; 
		lty = ( int.Parse(lr[0].UpdateDate.Split('/')[0]) - 1911 ).ToString();
		pty = ( int.Parse(pr[0].UpdateDate.Split('/')[0]) - 1911 ).ToString();
		cty = ( int.Parse(cr[0].UpdateDate.Split('/')[0]) - 1911 ).ToString();
		ltm = lr[0].UpdateDate.Split('/')[1];
		ptm = pr[0].UpdateDate.Split('/')[1];
		ctm = cr[0].UpdateDate.Split('/')[1];
		ltd = lr[0].UpdateDate.Split('/')[2];
		ptd = pr[0].UpdateDate.Split('/')[2];
		ctd = cr[0].UpdateDate.Split('/')[2];
		Regex title = new( "^臺中市立臺中第一高級中等學校學生自治聯合會|^臺中第一高級中學" ) ; 
		// Regex artno = new( "^第[]條" ) ; 
		Regex begsp = new( "^\\s*" ) ; 
		Regex cap = new( "^\\s*第\\s*[一二三四五六七八九十]+\\s*[編章節款目]{1}\\s*" ) ; 
		Regex para = new( @"^([1-9]{1}[0-9]+、|[1-9]{1}、|[１-９]{1}[０-９]+、|[１-９]{1}、|[1-9]{1}[0-9]+\u002E|[1-9]{1}\u002E|[１-９]{1}[０-９]+\u002E|[１-９]{1}\u002E|（[一二三四五六七八九十]{1}[一二三四五六七八九○零十百]+）|（[一二三四五六七八九十]{1}）|（[1-9]{1}[0-9]+）|（[1-9]{1}）|（[１-９]{1}[０-９]+）|（[１-９]{1}）|[一二三四五六七八九十]{1}[一二三四五六七八九○零十百]+、|[一二三四五六七八九十]{1}、)" ) ; 
		Regex nonch = new( @"[!-⬍⿰-㏿ﬓ-�\s]" ) ; 
		string nos = "<noscript>您必須開啟JavaScript才能使用本站完整功能</noscript>" ; 
		string foot = $$"""
</main>
<div class="toTop" tabindex="0" onclick="document.getElementById('top').scrollIntoView({behavior:'smooth'})"><span>回最</span><span>上方</span></div>
</div>
<footer>
<div class="footer-upper">
<ul>
<li><a href="laws?c=c" tabindex="9">中央法規</a></li>
<li><a href="laws?c=ex" tabindex="9">行政法規</a></li>
<li><a href="laws?c=l" tabindex="9">立法法規</a></li>
<li><a href="laws?c=j" tabindex="9">司法法規</a></li>
<li><a href="laws?c=el" tabindex="9">選舉法規</a></li>
<li><a href="cases" tabindex="9">判例查詢</a></li>
<li><a href="parties" tabindex="9">政黨查詢</a></li>
<li><a href="rel" tabindex="9">相關連結</a></li>
</ul>
</div>
<div class="footer-note">
<ul>
<li>本網站係提供法規之最新動態資訊及資料檢索，並不提供法規及法律諮詢之服務。</li>
<li>若有任何法律上的疑義，建議您可逕向發布法規之主管機關洽詢。</li>
<li>本網站法規資料係由各機關提供之電子檔或書面文字登打製作，若與各法規主管機關之公布文字有所不同，仍以各法規主管機關之公布資料為準。</li>
<li>部分資料內容，使用特殊文字或符號，如欲詳閱內容，請連結至<a tabindex="0" href="https://www.cns11643.gov.tw/downloadList.jsp?ID=1" target="_blank">全字庫</a>下載造字檔或<a tabindex="0" href="https://data.gov.tw/dataset/5961" target="_blank">政府資料開放平臺</a>下載全字庫字形檔。</li>
<li>建議使用<a href="https://www.google.com/intl/zh-TW/chrome/" target="_blank">Google Chrome</a>、<a href="https://brave.com/zh-tw/" target="_blank">Brave</a>或<a href="https://www.opera.com/" target="_blank">Opera</a>瀏覽本網頁；並建議使用電腦版<a href="https://www.google.com/intl/zh-TW/chrome/" target="_blank">Google Chrome</a>或<a href="https://brave.com/zh-tw/" target="_blank">Brave</a>列印。</li>
<li>法規整編資料截止日：民國 {{ lty }} 年 {{ ltm }} 月 {{ ltd }} 日</li>
<li>政黨整編資料截止日：民國 {{ pty }} 年 {{ ptm }} 月 {{ ptd }} 日</li>
<li>判例整編資料截止日：民國 {{ cty }} 年 {{ ctm }} 月 {{ ctd }} 日</li>
</ul>
</div>
<div class="footer-bottom">
<ul>
<li><a tabindex="0" href="open">資料開放宣告</a></li>
<li><a tabindex="0" href="print">法規彙編列印</a></li>
<li><a tabindex="0" href="s">統計</a></li>
</ul>
<span>本網站由臺中市立臺中第一高級中等學校學生自治聯合會學生會自治部及評議委員會共同維運管理</span><br/>
<span><span>市立臺中一中學聯會辦公室地址：</span><span>404009臺中市北區育才街2號 敬業樓3樓</span></span><br/>
<span>信箱：
<ul>
<li><span>學生會：</span><a tabindex="0" href="mailto:tcfshsa290801@gmail.com">tcfshsa290801@gmail.com</a></li>
<li><span>學生議會：</span><a tabindex="0" href="mailto:tcfshsc101@gmail.com">tcfshsc101@gmail.com</a></li>
<li><span>評議委員會：</span><a tabindex="0" href="mailto:tcfshstudentcourt@std.tcfsh.tc.edu.tw">tcfshstudentcourt@std.tcfsh.tc.edu.tw</a></li>
</ul>
</span>
</div>
</footer>
</body>
</html>
""" ; 
		using (StreamWriter o = File.AppendText("./html/index.html"))
		{
			o.WriteLine( Head( "首頁" ) ) ; 
			o.WriteLine( Bread( "" ) ) ; 
			o.WriteLine( "<div class=\"latest\">" ) ; 
			o.WriteLine( "<h1 tabindex=\"-1\">" ) ; 
			o.WriteLine( "最新消息" ) ; 
			o.WriteLine( "</h1>" ) ; 
			o.WriteLine( "<ul>" ) ; 
			for( int i = las!.Length - 1 ; i >= 0 ; -- i ) 
			{
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<a href=\"latest?no=" + ( i + 1 ) + "\">" ) ; 
				o.WriteLine( las[i].Title ) ; 
				o.WriteLine( "</a>" ) ; 
				o.WriteLine( "</li>" ) ; 
			}
			o.WriteLine( "</ul>" ) ; 
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( foot ) ; 
		}
		foreach( var l in laws ) 
		{
			// Directory.CreateDirectory($"./{now}/laws" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law/c" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law/x" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law/l" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law/j" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/law/e" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe/c" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe/x" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe/l" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe/j" ) ; 
			// Directory.CreateDirectory($"./{now}/laws/lawe/e" ) ; 
			// File.Copy($"./html/laws/law/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ,$"./{ now }/laws/law/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" , true ) ;
			File.Delete($"./html/laws/law/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ) ; 
			// File.Copy($"./html/laws/lawe/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ,$"./{ now }/laws/lawe/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" , true ) ;
			File.Delete($"./html/laws/lawe/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ) ; 
			using (StreamWriter o = File.AppendText($"./html/laws/law/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ) ) 
			{
				o.WriteLine( Head( title.Replace( l.LawName , "" ) ) ) ; 
				o.WriteLine( Bread( $"法規查詢:laws>{ l.LawName }" ) ) ; 
				o.WriteLine( """
<style>
div[ahhhhaaaahaha] h6 :first-child 
{
flex-shrink : 1 ; 
}
div[ahhhhaaaahaha] h6 a 
{
display : flex ; 
}
div[ahhhhaaaahaha] h6 a :first-child 
{
flex-shrink : 0 ; 
}
div[ahhhhaaaahaha] a:hover , div[ahhhhaaaahaha] a:focus 
{
color : #c03 ; 
text-decoration : none ; 
}
div[ahhhhaaaahaha] a , div[ahhhhaaaahaha] a:focus 
{
text-decoration : underline ; 
}
div[ahhhhaaaahaha] a:focus 
{
outline : .5px #fc0 solid ; 
border-radius : 1px ; 
}
</style>
""" ) ; 
				o.WriteLine($"<a href=\"{ l.LawURL.Replace( "laws/law" , "laws/lawe" ) }&c=&q=&l=&ab=\" class=\"printNoDisplay\">") ; 
				o.WriteLine( "簡讀版") ; 
				o.WriteLine( "</a>") ; 
				o.WriteLine( "<button onclick=\"window.print()\" class=\"printBtn\">列印</button>" ) ; 
				o.WriteLine( "<div ahhhhaaaahaha>" ) ; 
				o.WriteLine( "<div id=\"lawHeader\" class=\"title\">" ) ; 
				o.WriteLine( "<ul>" ) ; 
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "法規名稱：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "<span class=\"abandoned\">" ) ; 
				o.WriteLine( l.LawAbandonNote ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( l.LawName ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "修正日期：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine($"民國 { ( int.Parse( l.LawModifiedDate ) / 10000 ) - 1911 } 年 { ( int.Parse( l.LawModifiedDate ) % 10000 / 100 ).ToString("D2") } 月 { ( int.Parse( l.LawModifiedDate ) % 100 ).ToString("D2") } 日" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "法規類別：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( l.LawCategory ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				if( l.LawAttachments.Length > 0 ) 
				{
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "附件：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "<ul>" ) ; 
				foreach( var att in l.LawAttachments ) 
				{
					o.WriteLine( "<li>" ) ; 
					o.WriteLine($"<a href=\"{ att.FileURL }\">" ) ; 
					o.WriteLine( att.FileName ) ; 
					o.WriteLine( "</a>" ) ; 
					o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "</ul>" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "<li>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "立法沿革：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "<ol id=\"lHistory\" class=\"legislativeHistory\">" ) ; 
				foreach( var lhi in l.LawHistories.Split( "\r\n" ) ) 
				{
					o.WriteLine( "<li>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine($"{ lhi.Split( '.' )[0] }." ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( lhi.Split( '.' )[1] ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "</ol>" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				if( l.LawArticles.Where( a => a.ArticleType == "C" ).ToArray().Length > 0 ) 
				{
				o.WriteLine( "<li id=\"TableofC\">" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "目錄：" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "<span>" ) ; 
				o.WriteLine( "<ol>" ) ; 
				foreach( var lti in l.LawArticles.Where( a => a.ArticleType == "C" ) ) 
				{
					o.WriteLine($"<li onclick=\"document.getElementById('{ Regex.Replace( lti.ArticleContent , "\\s" , "" ) }').scrollIntoView({{behavior:'smooth'}})\">" ) ; 
					o.WriteLine($"<h{ begsp.Match( lti.ArticleContent ).Length / 3 + 1 }>" ) ; 
					o.WriteLine( lti.ArticleContent ) ; 
					o.WriteLine($"</h{ begsp.Match( lti.ArticleContent ).Length / 3 + 1 }>" ) ; 
					o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "</ol>" ) ; 
				o.WriteLine( "</span>" ) ; 
				o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "</ul>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<div id=\"mainC\">" ) ; 
				if( !string.IsNullOrEmpty( l.LawForeword ) ) 
				{
					o.WriteLine( "<div id=\"foreword\" class=\"Foreword\">" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( Array.IndexOf( laws , l ) == 0 ? "宗旨" : "前言" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<p>" ) ; 
					string[] f = l.LawForeword.Split( "\r\n" ) ; 
					for( int i = 0 ; i < f.Length ; ++ i ) 
					{
						o.WriteLine( f[i] ) ; 
						o.WriteLine( "<br />" ) ; 
					}
					o.WriteLine( "</p>" ) ; 
					o.WriteLine( "</div>" ) ; 
				}
				o.WriteLine( "<br />" ) ; 
				foreach( var a in l.LawArticles ) 
				{
					if( a.ArticleType == "C" ) 
					{
						int hi = begsp.Match( a.ArticleContent ).Length / 3 + 1 ; 
						o.WriteLine($"<h{ hi } id=\"{ Regex.Replace( a.ArticleContent , "\\s" , "" ) }\">" ) ; 
						o.WriteLine( "<pre>" ) ; 
						o.WriteLine( cap.Match( a.ArticleContent ) ) ; 
						o.WriteLine( "</pre>" ) ; 
						o.WriteLine( "<span>" ) ; 
						o.WriteLine( cap.Split( a.ArticleContent )[1] ) ; 
						o.WriteLine( "</span>" ) ; 
						o.WriteLine($"</h{ hi }>" ) ; 
					}
					else 
					{
						o.WriteLine( "<h6 id=\"" + a.ArticleNo + "\">" ) ; 
						o.WriteLine( "<a href=\"\">" ) ; 
						if( a.ArticleNo.Contains( '【' ) ) 
						{
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( a.ArticleNo.Split( '【' )[0] ) ; 
							o.WriteLine( "</span>" ) ; 
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( '【' + a.ArticleNo.Split( '【' )[1] ) ; 
							o.WriteLine( "</span>" ) ; 
						}
						else 
						{
							o.WriteLine( a.ArticleNo ) ; 
						}
						o.WriteLine( "</a>" ) ; 
						o.WriteLine( "</h6>" ) ; 
						string artc = "" ; 
						string[] aa = a.ArticleContent.Split( "\r\n" ) ; 
						int pcount = 0 ; 
						for( int i = 0 ; i < aa.Length ; ++ i ) 
						{
							artc += "<li id=\"\"" ; 
							if( !para.IsMatch( aa[i] ) ) 
							{
								artc += " class=\"p\">\n" ; 
								artc += aa[i] ; 
								++ pcount ; 
							}
							else 
							{
								artc += ">\n" ; 
								artc += "<span>" + para.Match( aa[i] ) + "</span><span>" + para.Replace( aa[i] , "" ) + "</span>" ; 
							}
							artc += "\n</li>\n" ; 
						}
						o.WriteLine( "<article" + ( pcount > 1 ? " class=\"showNum\"" : "" ) + ">" ) ; 
						o.Write( artc ) ; 
						o.WriteLine( "</article>" ) ; 
					}
					o.WriteLine( "<br />" ) ; 
				}
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( foot ) ; 
			}
			using (StreamWriter o = File.AppendText($"./html/laws/lawe/{ l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[0] }/{ int.Parse( l.LawURL.Replace( "https://tcfshsu.github.io/law/laws/law?a=" , "" )[1..] ) }.html" ) ) 
			{
				o.WriteLine( Head( title.Replace( l.LawName , "" ) ) ) ; 
				o.WriteLine( Bread( $"法規查詢:laws>{ l.LawName }（簡讀版）" ) ) ; 
				o.WriteLine( "<div ahhhhaaaahaha>" ) ; 
				o.WriteLine( "<div>" ) ; 
				o.WriteLine( "<div id=\"title\" class=\"title\">" ) ; 
				string nn = title.Split( l.LawName )[1] ; 
				o.Write($"<span class=\"abandoned\">{l.LawAbandonNote}</span><span>{title.Match( l.LawName )}<br />{nn}</span>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<ol id=\"lHistory\" class=\"legislativeHistory\">" ) ; 
				foreach( var his in l.LawHistories.Split( "\r\n" ) ) 
				{
					o.WriteLine( "<li>" + his + "</li>" ) ; 
				}
				o.WriteLine( "</ol>" ) ; 
				if( l.LawAttachments.Length > 0 ) 
				{
					o.WriteLine( "<li>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( "附件：" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( "<ul>" ) ; 
					foreach( var att in l.LawAttachments ) 
					{
						o.WriteLine( "<a href=\"" + att.FileURL + "\" style=\"text-decoration:underline;\">" ) ; 
						o.WriteLine( att.FileName ) ; 
						o.WriteLine( "</a>" ) ; 
					}
					o.WriteLine( "</ul>" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "</li>" ) ; 
					o.WriteLine( "<br />" ) ; 
				}
				if( !string.IsNullOrEmpty( l.LawForeword ) ) 
				{
					o.WriteLine( "<div id=\"foreword\" class=\"Foreword\">" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( Array.IndexOf( laws , l ) == 0 ? "宗旨" : "前言" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<p>" ) ; 
					string[] f = l.LawForeword.Split( "\r\n" ) ; 
					for( int i = 0 ; i < f.Length ; ++ i ) 
					{
						o.WriteLine( f[i] ) ; 
						o.WriteLine( "<br />" ) ; 
					}
					o.WriteLine( "</p>" ) ; 
					o.WriteLine( "</div>" ) ; 
				}
				o.WriteLine( "<br />" ) ; 
				foreach( var a in l.LawArticles ) 
				{
					if( a.ArticleType == "C" ) 
					{
						int hi = begsp.Match( a.ArticleContent ).Length / 3 + 1 ; 
						o.WriteLine($"<h{ hi } id=\"{ Regex.Replace( a.ArticleContent , "\\s" , "" ) }\">" ) ; 
						o.WriteLine( "<pre>" ) ; 
						o.WriteLine( cap.Match( a.ArticleContent ) ) ; 
						o.WriteLine( "</pre>" ) ; 
						o.WriteLine( "<span>" ) ; 
						o.WriteLine( cap.Split( a.ArticleContent )[1] ) ; 
						o.WriteLine( "</span>" ) ; 
						o.WriteLine($"</h{ hi }>" ) ; 
					}
					else 
					{
						o.WriteLine( "<h6 id=\"" + a.ArticleNo + "\">" ) ; 
						o.WriteLine( "<a href=\"\">" ) ; 
						if( a.ArticleNo.Contains( '【' ) ) 
						{
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( a.ArticleNo.Split( '【' )[0] ) ; 
							o.WriteLine( "</span>" ) ; 
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( '【' + a.ArticleNo.Split( '【' )[1] ) ; 
							o.WriteLine( "</span>" ) ; 
						}
						else 
						{
							o.WriteLine( a.ArticleNo ) ; 
						}
						o.WriteLine( "</a>" ) ; 
						o.WriteLine( "</h6>" ) ; 
						string artc = "" ; 
						string[] aa = a.ArticleContent.Split( "\r\n" ) ; 
						int pcount = 0 ; 
						for( int i = 0 ; i < aa.Length ; ++ i ) 
						{
							artc += "<li id=\"\"" ; 
							if( !para.IsMatch( aa[i] ) ) 
							{
								artc += " class=\"p\">\n" ; 
								artc += aa[i] ; 
								++ pcount ; 
							}
							else 
							{
								artc += ">\n" ; 
								artc += "<span>" + para.Match( aa[i] ) + "</span><span>" + para.Replace( aa[i] , "" ) + "</span>" ; 
							}
							artc += "\n</li>\n" ; 
						}
						o.WriteLine( "<article" + ( pcount > 1 ? " class=\"showNum\"" : "" ) + ">" ) ; 
						o.Write( artc ) ; 
						o.WriteLine( "</article>" ) ; 
					}
					o.WriteLine( "<br />" ) ; 
				}
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( foot ) ; 
			}
		}
		using (StreamWriter o = File.AppendText("./html/print.html"))
		{
			o.WriteLine( Head( "法規彙編" ) ) ; 
			o.WriteLine( Bread( "法規彙編列印" ) ) ; 
			o.WriteLine( "<div ahhhhaaaahaha>" ) ; 
			o.WriteLine( "<span id=\"COVER\">" ) ; 
			o.WriteLine( "<button onclick=\"window.print()\">列印</button>" ) ; 
			o.WriteLine( "<div>" ) ; 
			o.WriteLine( "臺中市立臺中第一高級中等學校" ) ; 
			o.WriteLine( "<br />" ) ; 
			o.WriteLine( "學生自治聯合會" ) ; 
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "法規彙編" ) ; 
			o.WriteLine( "</h1>" ) ; 
			o.WriteLine( "<br />" ) ; 
			o.WriteLine( "<span>" ) ; 
			o.WriteLine( "114-2" ) ; 
			o.WriteLine( "</span>" ) ; 
			o.WriteLine( "<small style=\"font-size:10pt;\">" ) ; 
			o.WriteLine( "（更新至 2025/10/11）" ) ; 
			o.WriteLine( "</small>" ) ; 
			o.WriteLine( "<br />" ) ; 
			o.WriteLine( "</span>" ) ; 
			L[][] law_by_c = [ laws.Where( l => !l.LawCategory.Contains( "選舉法規" ) && l.LawCategory.Contains( "中央法規" ) ).ToArray() , laws.Where( l => !l.LawCategory.Contains( "選舉法規" ) && l.LawCategory.Contains( "行政法規" ) ).ToArray() , laws.Where( l => !l.LawCategory.Contains( "選舉法規" ) && l.LawCategory.Contains( "立法法規" ) ).ToArray() , laws.Where( l => !l.LawCategory.Contains( "選舉法規" ) && l.LawCategory.Contains( "司法法規" ) ).ToArray() , laws.Where(l=>l.LawCategory.Contains("選舉法規")).ToArray() ] ; 
			int iii = 0 ;
			o.WriteLine( "<span>" ) ; 
			o.WriteLine( "<span class=\"title\">" ) ; 
			o.WriteLine( "目錄" ) ; 
			o.WriteLine( "</span>" ) ; 
			o.WriteLine( "<div id=\"A\">" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "中央法規" ) ; 
			o.WriteLine( "</h1>" ) ; 
			foreach( var n in law_by_c[0] ) 
			{
				o.Write($"<a href=\"#{ iii ++ }\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( n.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( n.LawName ) ; 
				o.WriteLine( "</a>" ) ; 
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<div id=\"B\">" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "行政法規" ) ; 
			o.WriteLine( "</h1>" ) ; 
			foreach( var n in law_by_c[1] ) 
			{
				o.Write($"<a href=\"#{ iii ++ }\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( n.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( n.LawName ) ; 
				o.WriteLine( "</a>" ) ; 
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<div id=\"C\">" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "立法法規" ) ; 
			o.WriteLine( "</h1>" ) ; 
			foreach( var n in law_by_c[2] ) 
			{
				o.Write($"<a href=\"#{ iii ++ }\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( n.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( n.LawName ) ; 
				o.WriteLine( "</a>" ) ; 
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<div id=\"D\">" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "司法法規" ) ; 
			o.WriteLine( "</h1>" ) ; 
			foreach( var n in law_by_c[3] ) 
			{
				o.Write($"<a href=\"#{ iii ++ }\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( n.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( n.LawName ) ; 
				o.WriteLine( "</a>" ) ; 
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<div id=\"E\">" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "選舉法規" ) ; 
			o.WriteLine( "</h1>" ) ; 
			foreach( var n in law_by_c[4] ) 
			{
				o.Write($"<a href=\"#{ iii ++ }\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( n.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( n.LawName ) ; 
				o.WriteLine( "</a>" ) ; 
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "</span>" ) ; 
			iii = 0 ; 
			foreach( var ll in law_by_c ) 
			{
			foreach( var l in ll ) 
			{
				o.WriteLine($"<div id=\"{ iii ++ }\">" ) ; 
				o.WriteLine( "<div>" ) ; 
				o.Write( "<div id=\"title\" class=\"title\">" ) ; 
				o.Write( "<span class=\"abandoned\">" ) ; 
				o.Write( l.LawAbandonNote ) ; 
				o.Write( "</span>" ) ; 
				o.Write( "<span>" ) ; 
				string nn = title.Split( l.LawName )[1] ; 
				o.Write( title.Match( l.LawName ) + "<br />" + nn ) ; 
				o.Write( "</span>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<ol id=\"lHistory\" class=\"legislativeHistory\">" ) ; 
				foreach( var his in l.LawHistories.Split( "\r\n" ) ) 
				{
					o.WriteLine( "<li>" ) ; 
					o.WriteLine( his ) ; 
					o.WriteLine( "</li>" ) ; 
				}
				o.WriteLine( "</ol>" ) ; 
				if( l.LawAttachments.Length > 0 ) 
				{
					o.WriteLine( "<li>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( "附件：" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( "<ul>" ) ; 
					foreach( var att in l.LawAttachments ) 
					{
						o.WriteLine( "<a href=\"" + att.FileURL + "\" style=\"text-decoration:underline;\">" ) ; 
						o.WriteLine( att.FileName ) ; 
						o.WriteLine( "</a>" ) ; 
					}
					o.WriteLine( "</ul>" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "</li>" ) ; 
					o.WriteLine( "<br />" ) ; 
				}
				if( !string.IsNullOrEmpty( l.LawForeword ) ) 
				{
					o.WriteLine( "<div id=\"foreword\" class=\"Foreword\">" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( Array.IndexOf( laws , l ) == 0 ? "宗旨" : "前言" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<p>" ) ; 
					string[] f = l.LawForeword.Split( "\r\n" ) ; 
					for( int i = 0 ; i < f.Length ; ++ i ) 
					{
						o.WriteLine( f[i] ) ; 
						o.WriteLine( "<br />" ) ; 
					}
					o.WriteLine( "</p>" ) ; 
					o.WriteLine( "</div>" ) ; 
				}
				o.WriteLine( "<br />" ) ; 
				foreach( var a in l.LawArticles ) 
				{
					if( a.ArticleType == "C" ) 
					{
						int hi = begsp.Match( a.ArticleContent ).Length / 3 + 1 ; 
						o.WriteLine( "<h" + hi + "><pre>" + cap.Match( a.ArticleContent ) + "</pre><span>" + cap.Split( a.ArticleContent )[1] + "</span></h" + hi + ">" ) ; 
					}
					else 
					{
						o.WriteLine( "<h6 id=\"" + a.ArticleNo + "\">" ) ; 
						o.WriteLine( "<a href=\"\">" ) ; 
						if( a.ArticleNo.Contains( '【' ) ) 
						{
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( a.ArticleNo.Split( '【' )[0] ) ; 
							o.WriteLine( "</span>" ) ; 
							o.WriteLine( "<span>" ) ; 
							o.WriteLine( '【' + a.ArticleNo.Split( '【' )[1] ) ; 
							o.WriteLine( "</span>" ) ; 
						}
						else 
						{
							o.WriteLine( a.ArticleNo ) ; 
						}
						o.WriteLine( "</a>" ) ; 
						o.WriteLine( "</h6>" ) ; 
						string artc = "" ; 
						string[] aa = a.ArticleContent.Split( "\r\n" ) ; 
						int pcount = 0 ; 
						for( int i = 0 ; i < aa.Length ; ++ i ) 
						{
							artc += "<li id=\"\"" ; 
							if( !para.IsMatch( aa[i] ) ) 
							{
								artc += " class=\"p\">\n" ; 
								artc += aa[i] ; 
								++ pcount ; 
							}
							else 
							{
								artc += ">\n" ; 
								artc += "<span>" + para.Match( aa[i] ) + "</span><span>" + para.Replace( aa[i] , "" ) + "</span>" ; 
							}
							artc += "\n</li>\n" ; 
						}
						o.WriteLine( "<article" + ( pcount > 1 ? " class=\"showNum\"" : "" ) + ">" ) ; 
						o.Write( artc ) ; 
						o.WriteLine( "</article>" ) ; 
					}
					o.WriteLine( "<br />" ) ; 
				}
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "</div>" ) ; 
			}
			}
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/guide.html"))
		{
			o.WriteLine( Head( "網站導覽" ) ) ; 
			o.WriteLine( """
<style>
#guide 
{
border-top : #0005 solid 2px ; 
}
#guide , #map 
{
padding-left : 1rem ; 
}
#guide ul 
{
padding-left : .5rem ; 
}
#guide ul li h3 
{
text-indent : -.5rem ; 
}
@media ( max-width : 768px ) 
{
#guide ul 
{
padding-left : 1rem ; 
}
}
#map dt 
{
display : block ; 
padding-left : 1rem ; 
}
#map dd 
{
display : block ; 
padding-left : 2rem ; 
}
</style>
""" ) ; 
			o.WriteLine( Bread( "網站導覽" ) ) ; 
			o.WriteLine( """
<h1>網站導覽</h1>
<div id="guide">
<h2>使用說明</h2>
<ul>
<li>
<h3>搜尋欄</h3>
<span>於法規條文、沿革、名稱、前言（宗旨）及附件檔案名稱中搜尋關鍵字，可以空格分隔多個關鍵字。</span>
</li>
</ul>
</div>
<div id="map">
<h2>網站地圖</h2>
<dl>
<dt><a href="laws">法規查詢</a></dt>
<dd><a href="laws?c=c">中央法規</a></dd>
<dd><a href="laws?c=ex">行政法規</a></dd>
<dd><a href="laws?c=l">立法法規</a></dd>
<dd><a href="laws?c=j">司法法規</a></dd>
<dd><a href="laws?c=el">選舉法規（含中央、行政及立法之選舉法規）</a></dd>
<dt><a href="cases">判例查詢</a></dt>
<dt><a href="parties">政黨查詢</a></dt>
<dt><a href="print">法規彙編（列印）</a></dt>
<dt><a href="s">統計</a></dt>
<dt><a href="rel">相關連結</a></dt>
<dt><a href="open">資料開放宣告</a></dt>
</dl>
</div>
<br class="printNoDisplay" />
<a class="printNoDisplay" href="">回首頁</a>
""" ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/open.html"))
		{
			o.WriteLine( Head( "資料開放宣告" ) ) ; 
			o.WriteLine( Bread( "資料開放宣告" ) ) ; 
			o.WriteLine( "<style>" ) ; 
			o.WriteLine( "p " ) ; 
			o.WriteLine( "{" ) ; 
			o.WriteLine( "margin-left : 2em ; " ) ; 
			o.WriteLine( "text-indent : -2em ; " ) ; 
			o.WriteLine( "margin-bottom : .5rem ; " ) ; 
			o.WriteLine( "}" ) ; 
			o.WriteLine( "</style>" ) ; 
			o.WriteLine( "<h1>" ) ; 
			o.WriteLine( "資料開放宣告" ) ; 
			o.WriteLine( "</h1>" ) ; 
			o.WriteLine( "<div class=\"open\">" ) ; 
			o.WriteLine( "<p>一、授權方式及範圍<br/>" ) ; 
			o.WriteLine( "為利各界廣為利用網站資料，中一中學聯會法規資料庫網站上刊載之所有資料與素材，其得受著作權保護之範圍，採<a href=\"https://data.gov.tw/license\" target=\"_blank\">政府資料開放授權條款-第1版</a>發布，以無償、非專屬、得由使用者再授權之方式提供公眾使用，使用者得不限時間及地域，重製、改作、編輯、公開傳輸或為其他方式之利用，開發各種產品或服務（簡稱加值衍生物），此一授權行為不會嗣後撤回，使用者亦無須取得本機關之書面或其他方式授權；然使用時應註明出處。</p>" ) ; 
			o.WriteLine( "<p>" ) ; 
			o.WriteLine( "二、相關事項說明" ) ; 
			o.WriteLine( "<br/>" ) ; 
			o.WriteLine( "<span style=\"margin-left:3em;text-indent:-3em;display:block;\">" ) ; 
			o.WriteLine( "（一）本宣告範圍僅及於著作權保護之範圍，不及於其他智慧財產權利（包括但不限於專利、商標、及機關標誌之提供）。" ) ; 
			o.WriteLine( "</span>" ) ; 
			o.WriteLine( "<br/>" ) ; 
			o.WriteLine( "<span style=\"margin-left:3em;text-indent:-3em;display:block;\">" ) ; 
			o.WriteLine( "（二）當事人自行公開或依法令公開之個人資料是否得被蒐集、處理，及利用，使用者須自行依照個人資料保護法之相關規定，規劃並執行法律要求之相應措施。" ) ; 
			o.WriteLine( "</span>" ) ; 
			o.WriteLine( "<br/>" ) ; 
			o.WriteLine( "<span style=\"margin-left:3em;text-indent:-3em;display:block;\">" ) ; 
			o.WriteLine( "（三）部分的影音、圖像、樂譜、專人專案撰文或其他著作，經機關特別聲明須經同意方可使用者，不在本宣告所及範圍，其後續使用應另經機關同意。" ) ; 
			o.WriteLine( "</span>" ) ; 
			o.WriteLine( "<br/>" ) ; 
			o.WriteLine( "</p>" ) ; 
			o.WriteLine( "<p>" ) ; 
			o.WriteLine( "三、應注意尊重第三人之著作人格權（包括姓名表示權及禁止不當變更權）。" ) ; 
			o.WriteLine( "</p>" ) ; 
			o.WriteLine( "<p>" ) ; 
			o.WriteLine( "四、使用本宣告提供之資料與素材，不得惡意變更其相關資訊，若利用後所展示之資訊與原資料與素材不符，且得被依法歸責，使用者須自負民事、刑事上之法律責任。" ) ; 
			o.WriteLine( "</p>" ) ; 
			o.WriteLine( "<p>" ) ; 
			o.WriteLine( "五、本網站之宣告，並不授與使用者代表本機關建議、認可或贊同其加值衍生物之地位。" ) ; 
			o.WriteLine( "</p>" ) ; 
			o.WriteLine( "</div>" ) ; 
			o.WriteLine( "<a class=\"printNoDisplay\" href=\"\">回首頁</a>" ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/rel.html"))
		{
			o.WriteLine( Head( "相關連結" ) ) ; 
			o.WriteLine( Bread( "相關連結" ) ) ; 
			o.WriteLine( """
<div class="rel">
<div class="tcfsh"><h1><a target="_blank" href="https://tcfsh.tc.edu.tw/">臺中市立臺中第一高級中等學校</a></h1></div>
<h2><a target="_blank" href="https://sites.google.com/view/tcfshsu/">臺中市立臺中第一高級中等學校學生自治聯合會</a></h2>
<h3>
<a target="_blank" href="https://law.moj.gov.tw/LawClass/LawSingle.aspx?pcode=H0060043&flno=53">學聯會設立之法源
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</h3>
<div class="tcfshsu">
<div class="tcfshsa">
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會">學生會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/秘書處">秘書處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/公活部">公活部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/自治部">自治部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/學權部">學權部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/新聞部">新聞部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/美宣部">美宣部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/學生會各部會/財政部">財政部
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
<li>
<a target="_blank" href="https://drive.google.com/drive/folders/1Erz6Ns45xVTRNS76Cs_zjDiOCkMczLVP?usp=sharing">學生會資訊公開處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://drive.google.com/drive/folders/186e7R1iAIiXKyu4nDJ5399llufd4bRHq">會長令
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生申訴陳情">學生申訴陳情
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.facebook.com/TCFSH.SA">學生會官方facebook
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.instagram.com/tcfsh.sa/">學生會官方Instagram
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
</div>
<div class="tcfshsc">
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會">學生議會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會組織/常務委員會">學生議會常務委員會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會組織/各委員會">學生議會各委員會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會組織/各處室/秘書處">學生議會秘書處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會組織/各處室/審計處">學生議會審計處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會組織/各處室/法制處">學生議會法制處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會監察使">議會監察使
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會監察使/會員友善專區">議會監察使 > 會員友善專區
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生議會/議會監察使/會員救濟專區">議會監察使 > 會員救濟專區
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.facebook.com/tcfshstudentcouncil">學生議會官方facebook
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.instagram.com/tcfsh.sc/">學生議會官方Instagram
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://youtube.com/channel/UClNJh-xl_sb0eoYwFoGnVew?si=CXsY33Y9ZseR8KRY">學生議會官方YouTube
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
</div>
<div class="tcfshsrc">
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/評議委員會">評議委員會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/評議委員會/評議委員">評議委員
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/評議委員會/評議委員會秘書處">評議委員會秘書處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/評議委員會/案件聲請">案件聲請
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83/%E6%A1%88%E4%BB%B6%E8%81%B2%E8%AB%8B#h.8loft3lrfg3">線上起訴
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83/%E6%A1%88%E4%BB%B6%E8%81%B2%E8%AB%8B#h.xtfes2nuhps">書狀範例
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/評議委員會/評議委員會資訊公開處">評議委員會資訊公開處
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83%E8%B3%87%E8%A8%8A%E5%85%AC%E9%96%8B%E8%99%95#h.u8k45ibhhdtj">公文
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://drive.google.com/drive/folders/1wCVyBrAjxW0_4H7e36d4oyZV-o387v4y">歷屆公文
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83%E8%B3%87%E8%A8%8A%E5%85%AC%E9%96%8B%E8%99%95#h.7npdv84zq5oz">評議委員會議公開
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
<li>
<a target="_blank" href="https://www.facebook.com/tcfsh.studentcourt">評議委員會官方facebook
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.instagram.com/tcfsh.src/">評議委員會官方Instagram
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
</div>
<div class="tcfshsa-indie">
<ul>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/獨立機關">學生會各獨立機關
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/獨立機關/學生會選舉委員會">學生會選舉委員會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.facebook.com/tcfsh.election.commission">學生會選舉委員會官方facebook
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.instagram.com/tcfsh.sec/">學生會選舉委員會官方Instagram
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.youtube.com/%40tcfshsec">學生會選舉委員會官方YouTube
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/學生會/獨立機關/學生會畢業生聯合會">學生會畢業生聯合會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
<li>
<a target="_blank" href="https://www.instagram.com/tcfshgcpt_87th/">學生會畢業生聯合會官方Instagram
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
</div>
<div class="lawall">
<h2>法規彙編下載</h2>
<ul>
<li><a href="print">本網站（列印）</a></li>
<li>
<a target="_blank" href="https://sites.google.com/view/tcfshsu/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83/%E8%A9%95%E8%AD%B0%E5%A7%94%E5%93%A1%E6%9C%83%E8%B3%87%E8%A8%8A%E5%85%AC%E9%96%8B%E8%99%95#h.1nevsowitfq">評議委員會
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor"
stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="ex-link">
<path d="M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6"></path>
<polyline points="15 3 21 3 21 9"></polyline>
<line x1="10" y1="14" x2="21" y2="3"></line>
</svg>
</a>
</li>
</ul>
</div>
</div>
</div>
<a class="printNoDisplay" href="">回首頁</a>
""" ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/s.html"))
		{
			o.WriteLine( Head( "統計" ) ) ; 
			o.WriteLine( Bread( "統計" ) ) ; 
			o.WriteLine( """
<style>
.statistics_table 
{
border : 1px #000 solid ; 
}
.statistics_table tr 
{
border-bottom : 1px #000 solid ; 
}
.statistics_table th , .statistics_table td 
{
border-right : 1px #000 solid ; 
}
</style>
<h1>統計</h1>
<h3>法規</h3>
<table class="statistics_table">
<thead style="font-family:'IBM Plex Mono', 'Sonti TC' , '標點' , 'Noto Serif KR' , 'Noto Serif TC' , 'Noto Serif JP' , 'Noto Serif SC' , sans-serif;">
<tr>
<th>字</th>
<th>unicode</th>
<th>出現次數</th>
<th>占比( ‰ )</th>
</tr>
</thead>
<tbody style="font-family:'Fira Code', 'Sonti TC' , '標點' , 'Noto Serif KR' , 'Noto Serif TC' , 'Noto Serif JP' , 'Noto Serif SC' , sans-serif;">
""" ) ; 
			string jj = nonch.Replace( json , "" ) ; 
			int wc = jj.Length ; 
			List<Result> lre = [] ; 
			for( int i = 0 ; i < wc ; ++ i ) 
			{
				int ii = jj.Count( j => j == jj[0] ) ; 
				lre.Add( new( jj[0] , ii ) ) ; 
				jj = jj.Replace( jj[0].ToString() , "" ) ; 
				if( string.IsNullOrEmpty( jj ) ) 
				{
					break ; 
				}
			}
			foreach( var x in lre.OrderByDescending(r => r.C).ThenBy(r => r.A) ) 
			{
				int xa = x.A;
				decimal xc = x.C * 1000;
				o.WriteLine( "<tr>" ) ; 
				o.WriteLine( "<td>" ) ; 
				o.WriteLine( x.A ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "<td style=\"min-width:calc(6em + 2px);\">" ) ; 
				o.WriteLine( "U+" + xa.ToString("X4") ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "<td>" ) ; 
				o.WriteLine( x.C ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "<td>" ) ; 
				o.WriteLine( xc / wc ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "</tr>" ) ; 
			}
			o.WriteLine( "" ) ; 
			o.WriteLine( """
</tbody>
</table>
<br />
<br />
<h3>判例</h3>
<table class="statistics_table">
<thead style="font-family:'IBM Plex Mono', 'Sonti TC' , '標點' , 'Noto Serif KR' , 'Noto Serif TC' , 'Noto Serif JP' , 'Noto Serif SC' , sans-serif;">
<tr>
<th>字</th>
<th>unicode</th>
<th>出現次數</th>
</tr>
</thead>
<tbody style="font-family:'Fira Code', 'Sonti TC' , '標點' , 'Noto Serif KR' , 'Noto Serif TC' , 'Noto Serif JP' , 'Noto Serif SC' , sans-serif;">
""" ) ; 
			string cc = nonch.Replace( cjson , "" ) ; 
			List<Result> cre = [] ; 
			for( int i = 0 ; i < cc.Length ; ++ i ) 
			{
				int ii = cc.Count( c => c == cc[0] ) ; 
				cre.Add( new( cc[0] , ii ) ) ; 
				cc = cc.Replace( cc[0].ToString() , "" ) ; 
				if( string.IsNullOrEmpty( cc ) ) 
				{
					break ; 
				}
			}
			foreach( var x in cre.OrderByDescending(r => r.C).ThenBy(r => r.A) ) 
			{
				int xa = x.A ; 
				o.WriteLine( "<tr>" ) ; 
				o.WriteLine( "<td>" ) ; 
				o.WriteLine( x.A ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "<td style=\"min-width:calc(6em + 2px);\">" ) ; 
				o.WriteLine( "U+" + xa.ToString("X4") ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "<td>" ) ; 
				o.WriteLine( x.C ) ; 
				o.WriteLine( "</td>" ) ; 
				o.WriteLine( "</tr>" ) ; 
			}
			o.WriteLine( """
</tbody>
</table>
""" ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/latest.html"))
		{
			o.WriteLine( Head( "最新消息" ) ) ; 
			o.WriteLine( nos ) ; 
			o.WriteLine( "<script>" ) ; 
			o.WriteLine( "const qa =  window.location.search.substring( 1 ).split( '&' )[0] ? window.location.search.substring( 1 ).split( '&' ) : [] ; " ) ; 
			o.WriteLine( "let qq = Array() ; " ) ; 
			o.WriteLine( "for( let q of qa ) " ) ; 
			o.WriteLine( "{" ) ; 
			o.WriteLine( "let temp = {} ; " ) ; 
			o.WriteLine( "temp[q.split( '=' )[0]] = q.split( '=' )[1] ; " ) ; 
			o.WriteLine( "qq.push( temp ) ; " ) ; 
			o.WriteLine( "} " ) ; 
			o.WriteLine( "const no = qq.filter( i => i.no )[0] ; " ) ; 
			o.WriteLine( "console.log( no ) ; " ) ; 
			o.WriteLine( "if( no ) " ) ; 
			o.WriteLine( "{" ) ; 
			o.WriteLine( "window.location.replace( \"latest/\" + no.no ) ; " ) ; 
			o.WriteLine( "}" ) ; 
			o.WriteLine( "</script>" ) ; 
			o.WriteLine( foot ) ; 
		}
		foreach( var la in las ) 
		{
			// File.Copy($"./html/latest/{ la.No }.html" ,$"./{ now }/latest/{ la.No }.html" , true ) ;
			File.Delete($"./html/latest/{ la.No }.html") ; 
			using (StreamWriter o = File.AppendText($"./html/latest/{ la.No }.html")) 
			{
				o.WriteLine( Head( "最新消息" ) ) ; 
				o.WriteLine( Bread( "最新消息" ) ) ; 
				o.WriteLine( "<dl class=\"la\">" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "日期" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( la.Date ) ; 
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "標題" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( la.Title.Replace( "\r\n" , "\n<br />\n" ) ) ; 
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "修正內容" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				foreach( var lc in la.Content ) 
				{
					o.WriteLine( "<dl>" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "修正法規" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine($"<a href=\"{ lc.LawURL }?f=ln{ la.No }\">" ) ; 
					o.WriteLine( lc.LawName ) ; 
					o.WriteLine( "</a>" ) ; 
					o.WriteLine( "</dd>" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<table>" ) ; 
					o.WriteLine( "<thead>" ) ; 
					o.WriteLine( "<tr>" ) ; 
					o.WriteLine( "<th>" ) ; 
					o.WriteLine( "條號" ) ; 
					o.WriteLine( "</th>" ) ; 
					o.WriteLine( "<th>" ) ; 
					o.WriteLine( "原條文" ) ; 
					o.WriteLine( "</th>" ) ; 
					o.WriteLine( "<th>" ) ; 
					o.WriteLine( "修正條文" ) ; 
					o.WriteLine( "</th>" ) ; 
					o.WriteLine( "<th>" ) ; 
					o.WriteLine( "理由" ) ; 
					o.WriteLine( "</th>" ) ; 
					o.WriteLine( "</tr>" ) ; 
					o.WriteLine( "</thead>" ) ; 
					o.WriteLine( "<tbody>" ) ; 
					foreach( var laa in lc.LawArticles ) 
					{
						o.WriteLine( "<tr>" ) ; 
						o.WriteLine( "<td>" ) ; 
						o.WriteLine( laa.ArticleNo ) ; 
						o.WriteLine( "</td>" ) ; 
						o.WriteLine( "<td>" ) ; 
						o.WriteLine( "<ul>" ) ; 
						foreach( var lp in laa.ArticleContent.Split( "\r\n" ) ) 
						{
							o.WriteLine( "<li>" ) ; 
							o.WriteLine( para.IsMatch( lp ) ?$"<span>{ para.Match( lp ) }</span><span>{ para.Replace( lp , "" ) }" : lp ) ; 
							o.WriteLine( "</li>" ) ; 
						}
						o.WriteLine( "</ul>" ) ; 
						o.WriteLine( "</td>" ) ; 
						o.WriteLine( "<td>" ) ; 
						o.WriteLine( "<ul>" ) ; 
						foreach( var lp in laa.Amendment.Split( "\r\n" ) ) 
						{
							o.WriteLine( "<li>" ) ; 
							o.WriteLine( para.IsMatch( lp ) ?$"<span>{ para.Match( lp ) }</span><span>{ para.Replace( lp , "" ) }" : lp ) ; 
							o.WriteLine( "</li>" ) ; 
						}
						o.WriteLine( "</ul>" ) ; 
						o.WriteLine( "</td>" ) ; 
						o.WriteLine( "<td>" ) ; 
						o.WriteLine( laa.Reason.Replace( "\r\n" , "<br />" ) ) ; 
						o.WriteLine( "</td>" ) ; 
						o.WriteLine( "</tr>" ) ; 
					}
					o.WriteLine( "</tbody>" ) ; 
					o.WriteLine( "</table>" ) ; 
					o.WriteLine( "</dd>" ) ; 
					o.WriteLine( "</dl>" ) ; 
				}
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "相關會長令" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				foreach( var lo in la.Orders ) 
				{
					o.WriteLine( "<dl>" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "檔案名稱" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine($"<a href=\"{ lo.FileURL }\" target=\"_blank\">" ) ; 
					o.WriteLine( lo.FileName ) ; 
					o.WriteLine( "</a>" ) ; 
					o.WriteLine( "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"ex-link\">" ) ; 
					o.WriteLine( "<path d=\"M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6\" />" ) ; 
					o.WriteLine( "<polyline points=\"15 3 21 3 21 9\" />" ) ; 
					o.WriteLine( "<line x1=\"10\" y1=\"14\" x2=\"21\" y2=\"3\" />" ) ; 
					o.WriteLine( "</svg>" ) ; 
					o.WriteLine( "</dd>" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "主要內容" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( lo.Main ) ; 
					o.WriteLine( "</dd>" ) ; 
					o.WriteLine( "</dl>" ) ; 
				}
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "</dl>" ) ; 
				o.WriteLine( "<a class=\"printNoDisplay\" href=\"\">回首頁</a>" ) ; 
				o.WriteLine( foot ) ; 
			}
		}
		using (StreamWriter o = File.AppendText("./html/parties.html"))
		{
			o.WriteLine( Head( "政黨查詢" ) ) ; 
			o.WriteLine( Bread( "政黨查詢" ) ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/parties/detail.html"))
		{
			o.WriteLine( Head( "" ) ) ; 
			o.WriteLine( Bread( "政黨查詢:parties>政黨詳細資料" ) ) ; 
			o.WriteLine( foot ) ; 
		}
		foreach( var p in parties ) 
		{
			File.Delete($"./html/parties/detail/{ p.Party }.html" ) ; 
			using (StreamWriter o = File.AppendText($"./html/parties/detail/{ p.Party }.html" ))
			{
				o.WriteLine( Head( p.PartyName ) ) ; 
				o.WriteLine( Bread( "政黨查詢:parties>政黨詳細資料" ) ) ; 
				o.WriteLine( "<h1>" ) ; 
				o.WriteLine( p.PartyName ) ; 
				o.WriteLine( "</h1>" ) ; 
				o.WriteLine( "<div id=\"pd\">" ) ; 
				o.WriteLine( "<div class=\"pl\">" ) ; 
				o.WriteLine($"<img src=\"https://tcfshsu.github.io/law/i/parties/{ p.Logo[0] }\" alt=\"{ p.PartyName }標章\" />" ) ; 
				if( p.Logo.Length == 2 ) 
				{
					o.WriteLine($"<audio class=\"printNoDisplay\" controls src=\"https://tcfshsu.github.io/law/A/parties/{ p.Logo[1] }\">" ) ; 
					o.WriteLine( "</audio>" ) ; 
				}
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<div class=\"pt\">" ) ; 
				o.WriteLine( "<dl>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "政黨名稱" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( p.PartyName ) ; 
				o.WriteLine( "</dd>" ) ; 
				if( !string.IsNullOrEmpty( p.PartyAbbreviation ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "政黨簡稱" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( p.PartyAbbreviation ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "政黨負責人" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( p.Chairman ) ; 
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "狀態" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( p.PartyState ) ; 
				o.WriteLine( "</dd>" ) ; 
				if( !string.IsNullOrEmpty( p.PartyRegisteredDate ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "註冊日期" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( p.PartyRegisteredDate ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( p.PartyEstablishedDate ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "成立日期" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( p.PartyEstablishedDate ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( p.PartyURL ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "政黨官網" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine($"<a href=\"{ p.PartyURL }\" target=\"_blank\">" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( p.PartyURL) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"ex-link\">" ) ; 
					o.WriteLine( "<path d=\"M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6\" />" ) ; 
					o.WriteLine( "<polyline points=\"15 3 21 3 21 9\" />" ) ; 
					o.WriteLine( "<line x1=\"10\" y1=\"14\" x2=\"21\" y2=\"3\" />" ) ; 
					o.WriteLine( "</svg>" ) ; 
					o.WriteLine( "</a>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				o.WriteLine( "</dl>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<br class=\"printNoDisplay\">" ) ; 
				o.WriteLine( "<a class=\"printNoDisplay\" href=\"parties\">" ) ; 
				o.WriteLine( "回上一頁" ) ; 
				o.WriteLine( "</a>" ) ; 
				o.WriteLine( foot ) ; 
			}
		}
		using (StreamWriter o = File.AppendText("./html/cases.html"))
		{
			o.WriteLine( Head( "判例查詢" ) ) ; 
			o.WriteLine( Bread( "判例查詢" ) ) ; 
			o.WriteLine( foot ) ; 
		}
		using (StreamWriter o = File.AppendText("./html/cases/detail.html"))
		{
			o.WriteLine( Head( "" ) ) ; 
			o.WriteLine( Bread( "判例查詢:cases>判例詳細資料" ) ) ; 
			o.WriteLine( foot ) ; 
		}
		foreach( var c in cases ) 
		{
			File.Delete($"./html/cases/detail/{ c.No }.html");
			using (StreamWriter o = File.AppendText($"./html/cases/detail/{ c.No }.html"))
			{
				o.WriteLine( Head( c.No ) ) ; 
				o.WriteLine( Bread( "判例查詢:cases>判例詳細資料" ) ) ; 
				o.WriteLine( "<h1 tabindex=\"-1\">" ) ; 
				o.WriteLine( c.No ) ; 
				o.WriteLine( "</h1>" ) ; 
				o.WriteLine( "<div class=\"cd\">" ) ; 
				o.WriteLine( "<dl>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "<h4>" ) ; 
				o.WriteLine( "判決字號" ) ; 
				o.WriteLine( "</h4>" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( "<h5>" ) ; 
				o.WriteLine( c.No ) ; 
				o.WriteLine( "</h5>" ) ; 
				o.WriteLine( "</dd>" ) ; 
				o.WriteLine( "<dt>" ) ; 
				o.WriteLine( "<h4>" ) ; 
				o.WriteLine( "類別" ) ; 
				o.WriteLine( "</h4>" ) ; 
				o.WriteLine( "</dt>" ) ; 
				o.WriteLine( "<dd>" ) ; 
				o.WriteLine( "<h5>" ) ; 
				o.WriteLine( c.Category ) ; 
				o.WriteLine( "</h5>" ) ; 
				o.WriteLine( "</dd>" ) ; 
				if( !string.IsNullOrEmpty( c.DeliberationDate ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "判決日期" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.DeliberationDate ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Petitioner ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "聲請人" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Petitioner ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Plaintiff ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "原告" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Plaintiff ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Defendant ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "被告" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Defendant ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Title ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "標題" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Title ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Cause ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "案由" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Cause ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.Syllabus ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "主文" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.Syllabus ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.State ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "狀態" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<dd>" ) ; 
					o.WriteLine( "<h5>" ) ; 
					o.WriteLine( c.State ) ; 
					o.WriteLine( "</h5>" ) ; 
					o.WriteLine( "</dd>" ) ; 
				}
				if( !string.IsNullOrEmpty( c.FullJudgement ) ) 
				{
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine($"<a href=\"{ c.FullJudgement }\" target=\"_blank\">" ) ; 
					o.WriteLine( "<span>" ) ; 
					o.WriteLine( "判決全文" ) ; 
					o.WriteLine( "</span>" ) ; 
					o.WriteLine( "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"ex-link\">" ) ; 
					o.WriteLine( "<path d=\"M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6\" />" ) ; 
					o.WriteLine( "<polyline points=\"15 3 21 3 21 9\" />" ) ; 
					o.WriteLine( "<line x1=\"10\" y1=\"14\" x2=\"21\" y2=\"3\" />" ) ; 
					o.WriteLine( "</svg>" ) ; 
					o.WriteLine( "</a>" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
				}
				if( c.Rulings.Length > 0 ) 
				{
					o.WriteLine( "" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "裁定" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<div class=\"cd r\">" ) ; 
					foreach( var cc in c.Rulings ) 
					{
						o.WriteLine( "<dl>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<h5>" ) ; 
						o.WriteLine( "裁定字號" ) ; 
						o.WriteLine( "</h5>" ) ; 
						o.WriteLine( "</dt>" ) ; 
						o.WriteLine( "<dd>" ) ; 
						o.WriteLine( "<h6>" ) ; 
						o.WriteLine( cc.RulingNo ) ; 
						o.WriteLine( "</h6>" ) ; 
						o.WriteLine( "</dd>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<h5>" ) ; 
						o.WriteLine($"<a href=\"{ cc.FullRulingURL }\" target=\"_blank\">" ) ; 
						o.WriteLine( "<span>" ) ; 
						o.WriteLine( "裁定全文" ) ; 
						o.WriteLine( "</span>" ) ; 
						o.WriteLine( "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"ex-link\">" ) ; 
						o.WriteLine( "<path d=\"M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6\" />" ) ; 
						o.WriteLine( "<polyline points=\"15 3 21 3 21 9\" />" ) ; 
						o.WriteLine( "<line x1=\"10\" y1=\"14\" x2=\"21\" y2=\"3\" />" ) ; 
						o.WriteLine( "</svg>" ) ; 
						o.WriteLine( "</a>" ) ; 
						o.WriteLine( "</h5>" ) ; 
						o.WriteLine( "</dt>" ) ; 
						o.WriteLine( "</dl>" ) ; 
					}
					o.WriteLine( "</div>" ) ; 
				}
				if( c.Opinions.Length > 0 ) 
				{
					o.WriteLine( "" ) ; 
					o.WriteLine( "<dt>" ) ; 
					o.WriteLine( "<h4>" ) ; 
					o.WriteLine( "意見書" ) ; 
					o.WriteLine( "</h4>" ) ; 
					o.WriteLine( "</dt>" ) ; 
					o.WriteLine( "<div class=\"cd o\">" ) ; 
					foreach( var cc in c.Opinions ) 
					{
						o.WriteLine( "<dl>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<h5>" ) ; 
						o.WriteLine( "類型" ) ; 
						o.WriteLine( "</h5>" ) ; 
						o.WriteLine( "</dt>" ) ; 
						o.WriteLine( "<dd>" ) ; 
						o.WriteLine( "<h6>" ) ; 
						o.WriteLine( cc.Type ) ; 
						o.WriteLine( "</h6>" ) ; 
						o.WriteLine( "</dd>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<h5>" ) ; 
						o.WriteLine( "委員" ) ; 
						o.WriteLine( "</h5>" ) ; 
						o.WriteLine( "</dt>" ) ; 
						o.WriteLine( "<dd>" ) ; 
						o.WriteLine( "<h6>" ) ; 
						o.WriteLine( cc.Member ) ; 
						o.WriteLine( "</h6>" ) ; 
						o.WriteLine( "</dd>" ) ; 
						o.WriteLine( "<dt>" ) ; 
						o.WriteLine( "<h5>" ) ; 
						o.WriteLine($"<a href=\"{ cc.URL }\" target=\"_blank\">" ) ; 
						o.WriteLine( "<span>" ) ; 
						o.WriteLine( "意見書全文" ) ; 
						o.WriteLine( "</span>" ) ; 
						o.WriteLine( "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"ex-link\">" ) ; 
						o.WriteLine( "<path d=\"M18 13v6a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h6\" />" ) ; 
						o.WriteLine( "<polyline points=\"15 3 21 3 21 9\" />" ) ; 
						o.WriteLine( "<line x1=\"10\" y1=\"14\" x2=\"21\" y2=\"3\" />" ) ; 
						o.WriteLine( "</svg>" ) ; 
						o.WriteLine( "</a>" ) ; 
						o.WriteLine( "</h5>" ) ; 
						o.WriteLine( "</dt>" ) ; 
						o.WriteLine( "</dl>" ) ; 
					}
					o.WriteLine( "</div>" ) ; 
				}
				o.WriteLine( "</dl>" ) ; 
				o.WriteLine( "</div>" ) ; 
				o.WriteLine( "<a class=\"printNoDisplay\" href=\"cases\">" ) ; 
				o.WriteLine( "回上一頁" ) ; 
				o.WriteLine( "</a>" ) ; 
				o.WriteLine( foot ) ; 
			}
		}
	}

// ------------------------------------------------------------------
// |    classes                                                     |
// ------------------------------------------------------------------
	public class LawRoot
	{
		required public string UpdateDate { get; set; }
		required public L[] Laws { get; set; }
	}
	public class L
	{
		required public string LawLevel { get; set; }
		required public string LawName { get; set; }
		required public string LawURL { get; set; }
		required public string LawCategory { get; set; }
		required public string LawModifiedDate { get; set; }
		required public string LawEffectiveDate { get; set; }
		required public string LawEffectiveNote { get; set; }
		required public string LawAbandonNote { get; set; }
		required public string LawHasEngVersion { get; set; }
		required public string EngLawName { get; set; }
		required public Attachments[] LawAttachments { get; set; }
		required public string LawHistories { get; set; }
		required public string LawForeword { get; set; }
		required public A[] LawArticles { get; set; }
	}
	public class A
	{
		required public string ArticleType { get; set; }
		required public string ArticleNo { get; set; }
		required public string ArticleContent { get; set; }
		public Cases[]? Cases { get; set; }
		public Rel[]? Rel { get; set; }
		public Ref[]? Ref { get; set; }
	}
	public class Attachments
	{
		required public string FileName { get; set; }
		required public string FileURL { get; set; }
	}
	public class Cases
	{
		required public string CaseNo { get; set; }
		required public string CaseUrl { get; set; }
	}
	public class Rel
	{
		required public string Name { get; set; }
		required public string Url { get; set; }
	}
	public class Ref
	{
		required public string Name { get; set; }
		required public string Url { get; set; }
	}
	public class Latests
	{
		[System.Text.Json.Serialization.JsonNumberHandling((System.Text.Json.Serialization.JsonNumberHandling)1)]
		public int No { get; set; } 
		public required string Date { get; set; }
		public required string Title { get; set; } 
		required public C[] Content { get; set; } 
		required public O[] Orders { get; set; } 
	}
	public class C 
	{
		public required string LawName { get; set; }
		public required string LawURL { get; set; }
		required public string LawEffectiveDate { get; set; }
		required public string LawEffectiveNote { get; set; }
		required public string LawAbandonNote { get; set; }
		public required Am[] LawArticles { get; set; }
	}
	public class Am
	{
		public required string ArticleType { get; set; }
		required public string ArticleNo { get; set; }
		required public string ArticleContent { get; set; }
		public required string Amendment { get; set; } 
		required public string Reason { get; set; } 
	}
	public class O 
	{
		public required string FileName { get; set; } 
		public required string Main { get; set; } 
		public required string FileURL { get; set; } 
	}
	public class Py
	{
		public required string UpdateDate { get; set; }
		public required P[] Parties { get; set; }
	}
	public class P 
	{
		[System.Text.Json.Serialization.JsonNumberHandling((System.Text.Json.Serialization.JsonNumberHandling)1)]
		public int Party { get; set; }
		public required string PartyName { get; set; }
		public required string PartyAbbreviation { get; set; }
		public required string[] Logo { get; set; }
		public required string Chairman { get; set; }
		public required string PartyState { get; set; }
		public required string PartyEstablishedDate { get; set; }
		public required string PartyRegisteredDate { get; set; }
		public required string PartyURL { get; set; }
	}
	public class Casess
	{
		public required string UpdateDate { get; set; }
		public required Case[] Cases { get; set; }
	}
	public class Case 
	{
		public required string No { get; set; }
		public required string Category { get; set; }
		public required string DeliberationDate { get; set; }
		public required string Petitioner { get; set; }
		public required string Plaintiff { get; set; }
		public required string Defendant { get; set; }
		public required string Title { get; set; }
		public required string Cause { get; set; }
		public required string Syllabus { get; set; }
		public required string State { get; set; }
		public required string FullJudgement { get; set; }
		public required Ru[] Rulings { get; set; }
		public required Op[] Opinions { get; set; } 
	}
	public class Ru 
	{
		public required string RulingNo { get; set; }
		public required string FullRulingURL { get; set; }
	}
	public class Op 
	{
		public required string Type { get; set; }
		public required string Member { get; set; }
		public required string URL { get; set; }
	}
	public class Result( char aa , int cc ) 
	{
		public char A { get; set; } = aa ; 
		public int C { get; set; } = cc ; 
	}
}
