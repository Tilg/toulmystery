if(typeof unityObject=="undefined"){
	var unityObject=function() {
		var a="Unity Player",
			s="application/vnd.unity",
			j=window,
			S=document,
			P=navigator,
			B=false,
			R=[],
			F=[],
			V=[],
			h=null,
			k=null,
			E=true,
			W=true,
			U="http://webplayer.unity3d.com/download_webplayer-3.x/",
			M=false,
			g=false,
			z=true,
			l=false,
			x=[],
			t=true,
			A=false,
			w=true,
			Q=false,
			K="unknown",
			p=function(){
				var aa=P.userAgent,
					ac=P.platform;
				var ad={
						w3:typeof S.getElementById!="undefined"&&typeof S.getElementsByTagName!="undefined"&&typeof S.createElement!="undefined",
						win:ac?/win/i.test(ac):/win/i.test(aa),
						mac:ac?/mac/i.test(ac):/mac/i.test(aa),
						ie:/msie [0-9]+(\.[0-9]+)*/i.test(aa),
						ff:/firefox/i.test(aa),
						ch:/chrome/i.test(aa),
						wk:/webkit/i.test(aa)?parseFloat(aa.replace(/^.*webkit\/(\d+(\.\d+)?).*$/i,"$1")):false
					};
				var Y=S.getElementsByTagName("script");
				for(var ab=0;ab<Y.length;++ab){
					var X=Y[ab].src.match(/^(.*)3\.0\/uo\/UnityObject\.js$/i);
					if(X){
						U=X[1];
						break
					}
				}
				function Z(ag,af){
					for(var ah=0;ah<Math.max(ag.length,af.length);++ah){
						var ae=(ah<ag.length)?new Number(ag[ah]):0;
						var ai=(ah<af.length)?new Number(af[ah]):0;
						if(ae<ai){return -1}
						if(ae>ai){return 1}
					}
					return 0
				}
				ad.java=function(){
					if(P.javaEnabled()){
						if(ad.win){
							if(ad.ff||ad.ch){
								if(typeof P.mimeTypes!="undefined"){
									var ah=[1,6,0,12];
									for(var ag=0;ag<P.mimeTypes.length;++ag){
										if(P.mimeTypes[ag].enabledPlugin){
											var ae=P.mimeTypes[ag].type.match(/^application\/x-java-applet;jpi-version=(\d+)(?:\.(\d+)(?:\.(\d+)(?:_(\d+))?)?)?$/);
											if(ae!=null){
												if(Z(ah,ae.slice(1))<=0){return true}
											}
										}
									}
								}
							}
							else{
								if(ad.ie){
									if(typeof ActiveXObject!="undefined"){
										function af(ai){
											try{return new ActiveXObject("JavaWebStart.isInstalled."+ai+".0")!=null}
											catch(aj){return false}
										}
										return af("1.7.0")||af("1.6.0")||af("1.5.0")||af("1.4.2")
									}
								}
							}
						}
					}
					return false
				}
				();
				ad.co=function(){
					if(ad.win&&ad.ie){
						var ae=aa.match(/\.NET CLR [0-9.]+/g);
						if(ae!=null){
							function ah(ai){
								return ai.match(/([0-9]+)\.([0-9]+)\.([0-9]+)/i).slice(1)
							}
							var ag=[3,5,0];
							for(var af=0;af<ae.length;++af){
								if(Z(ag,ah(ae[af]))<=0){return true}
							}
						}
					}
					return false
				}
				();
				return ad
			}(),
			G=function(){
			if(!p.w3){return}
			if((typeof S.readyState!="undefined"&&S.readyState=="complete")||(typeof S.readyState=="undefined"&&(S.getElementsByTagName("body")[0]||S.body))){H()}
			if(B){return}
			if(typeof S.addEventListener!="undefined"){S.addEventListener("DOMContentLoaded",H,false)}
			if(p.win&&p.ie){
				S.attachEvent("onreadystatechange",
							  function(){
								if(S.readyState=="complete"){
									S.detachEvent("onreadystatechange",arguments.callee);
									H()
								}
							  }
							  );
				if(j==top){
					(function(){
						if(B){return}
						try{S.documentElement.doScroll("left")}
						catch(X){
							setTimeout(arguments.callee,10);
							return
						}
						H()
					 }
					)
					()
				}
			}
			if(p.wk){
				(function(){
					if(B){return}
					if(!/loaded|complete/.test(S.readyState)){
						setTimeout(arguments.callee,10);
						return
					}
					H()
				 }
				)
				()
			}
			v(H)
		}();
		function b(X){
			if(B){X()}
			else{R[R.length]=X}
		}
		function H(){
			if(B){return}
			try{
				var X=S.getElementsByTagName("body")[0];
				var Y=X.appendChild(S.createElement("span"));
				X.removeChild(Y)
			}
			catch(aa){return}
			B=true;
			for(var Z=0;Z<R.length;++Z){R[Z]()}
		}
		function v(Y){
			if(typeof j.addEventListener!="undefined"){j.addEventListener("load",Y,false)}
			else{
				if(typeof S.addEventListener!="undefined"){S.addEventListener("load",Y,false)}
				else{
					if(typeof j.attachEvent!="undefined"){u(j,"onload",Y)}
					else{
						if(typeof window.onload=="function"){
							var X=window.onload;
							j.onload=function(){
								X();
								Y()
							}
						}
						else{j.onload=Y}
					}
				}
			}
		}
		function u(Y,X,Z){
			Y.attachEvent(X,Z);
			V[V.length]={target:Y,type:X,event:Z}
		}
		function N(Z){
			if(!w){return}
			var X=window._gaq=window._gaq||[];
			if(!Q){
				X.push(["unity._setAccount","UA-16068464-10"]);
				X.push(["unity._setCustomVar",1,"Revision",51248,2])
			}
			X.push(["unity._trackPageview","/webplayer/install/"+Z]);
			if(!Q){
				var Y=S.getElementsByTagName("script");
				for(var ab=0;ab<Y.length;++ab){
					if(Y[ab].src.match(/^(.*)\.google-analytics.com\/ga\.js$/i)){
						Q=true;
						break
					}
				}
				if(!Q){
					Q=true;
					var aa=S.createElement("script");
					aa.type="text/javascript";
					aa.async=true;
					aa.src=("https:"==S.location.protocol?"https://ssl":"http://www")+".google-analytics.com/ga.js";
					var ac=document.getElementsByTagName("script")[0];
					ac.parentNode.insertBefore(aa,ac)
				}
			}
		}
		function d(X) {
			if(K=="unknown"&&X=="missing"){
				N(X)
			}
			else{
				if(X=="installed"){
					if(K=="missing"){
						N(X);
						K="updating"
					}
					return
				}
				else{
					if(K=="updating"&&X=="running"){N(X)}
				}
			}
			K=X
		}
		function q(ab){
			var ae=0;
			if(ab){
				var aa=ab.toLowerCase().match(/^(\d+)(?:\.(\d+)(?:\.(\d+)([dabfr])?(\d+)?)?)?$/);
				if(aa&&aa[1]){
					var Z=aa[1];
					var ac=aa[2]?aa[2]:0;
					var ad=aa[3]?aa[3]:0;
					var X=aa[4]?aa[4]:"r";
					var Y=aa[5]?aa[5]:0;
					ae|=((Z/10)%10)<<28;
					ae|=(Z%10)<<24;
					ae|=(ac%10)<<20;
					ae|=(ad%10)<<16;
					ae|={
						d:2<<12,
						a:4<<12,
						b:6<<12,
						f:8<<12,
						r:8<<12
					}
					[X];
					ae|=((Y/100)%10)<<8;
					ae|=((Y/10)%10)<<4;
					ae|=(Y%10)
				}
			}
			return ae
		}
		function L(aa){
			var Z=S.getElementsByTagName("body")[0];
			var Y=S.createElement("object");
			if(Z&&Y){
				Y.setAttribute("type",s);
				Y.style.visibility="hidden";
				Z.appendChild(Y);
				var X=0;
				(function(){
					if(typeof Y.GetPluginVersion=="undefined"){
						if(X++<10){setTimeout(arguments.callee,10)}
						else{
							Z.removeChild(Y);
							aa(null)
						}
					}
					else{
						var ab=Y.GetPluginVersion();
						Z.removeChild(Y);aa(ab)
					}
				 }
				)
				()
			}
			else{aa(null)}
		}
		function C(ac){
			var Z="missing";
			P.plugins.refresh();
			if(typeof P.plugins!="undefined"&&P.plugins[a]&&typeof P.mimeTypes!="undefined"&&P.mimeTypes[s]&&P.mimeTypes[s].enabledPlugin){
				Z="installed";
				if(/Safari/.test(P.appVersion)&&/Mac OS X 10_6/.test(P.appVersion)){
					L(function(ad){
						if(!ad){Z="broken"}
						d(Z);
						ac(Z)
					  }
					);
					return
				}
				else{
					if(p.mac&&p.ch){
						L(function(ad){
							if(q(ad)<=q("2.6.1f3")){
								Z="broken"
							}
							d(Z);
							ac(Z)
						  }
						);
						return
					}
				}
			}
			else{
				if(typeof j.ActiveXObject!="undefined"){
					try{
						var aa=new ActiveXObject("UnityWebPlayer.UnityWebPlayer.1").GetPluginVersion();
						Z="installed";
						if(aa=="2.5.0f5"){
							var X=/Windows NT \d+\.\d+/.exec(P.userAgent);
							if(X&&X.length>0){
								var Y=parseFloat(X[0].split(" ")[2]);
								if(Y>=6){Z="broken"}
							}
						}
					}
					catch(ab){}
				}
			}
			d(Z);
			ac(Z)
		}
		function y(X){
			if(/^[-+]?[0-9]+$/.test(X)){X+="px"}
			return X
		}
		function n(Y,Z,aa,ag){
			var ae=S.getElementById(Y);
			if(!ae){
				if(ag){
					ag({success:false,id:Y})
				}
				return
			}
			if(p.win&&p.ie){
				var af="";
				for(var X in Z){
					if(Z[X]!=Object.prototype[X]){
						if(X.toLowerCase()=="styleclass"){
							af+=' class="'+Z[X]+'"'
						}
						else{
							if(X.toLowerCase()!="classid"){
								af+=" "+X+'="'+Z[X]+'"'
							}
						}
					}
				}
				var ad="";
				for(var X in aa){
					if(aa[X]!=Object.prototype[X]){
						if(X.toLowerCase()!="classid"){
							ad+='<param name="'+X+'" value="'+aa[X]+'" />'
						}
					}
				}
				ae.outerHTML='<div id="'+Y+'" style="width: '+y(Z.width)+"; height: "+y(Z.height)+'; visibility: hidden;">'+
								'<object classid="clsid:444785F1-DE89-4295-863A-D46C3A781394" style="display: block; width: 100%; height: 100%;"'+af+">"+
									ad+
								"</object>"+
							"</div>";
				F[F.length]=Y
			}
			else{
				var ab=S.createElement("div");
				ab.setAttribute("id",Y);
				ab.style.width=y(Z.width);
				ab.style.height=y(Z.height);
				ab.style.visibility="hidden";
				var ac=S.createElement("embed");
				ac.setAttribute("type",s);
				ac.style.display="block";
				ac.style.width="100%";
				ac.style.height="100%";
				for(var X in Z){
					if(Z[X]!=Object.prototype[X]){
						if(X.toLowerCase()=="styleclass"){
							ac.setAttribute("class",Z[X])
						}
						else{
							if(X.toLowerCase()!="classid"){
								ac.setAttribute(X,Z[X])
							}
						}
					}
				}
				for(var X in aa){
					if(aa[X]!=Object.prototype[X]){
						if(X.toLowerCase()!="classid"){
							ac.setAttribute(X,aa[X])
						}
					}
				}
				ab.appendChild(ac);
				ae.parentNode.replaceChild(ab,ae)
			}
			f(
			  Y,function(ah){
				  if(ah){
					ah.parentNode.style.visibility="visible";
					ah.focus()
				  }
				  else{I(Y)}
				  if(ag){
					ag({success:Boolean(ah),id:Y,ref:ah})
				  }
				}
			)
		}
		function J(){
			return M?"UnityWebPlayerFull.exe":"UnityWebPlayer.exe"
		}
		function m(Z,ab,al,ah,aq){
			var av=S.getElementById(Z);
			if(av){
				var Y="standard";
				if(z&&p.java&&!l){
					x[Z]={attributes:ab,params:al,callback:ah,broken:aq};
					Y="java";
					if(g){
						if(ah){ ah({success:false,id:Z}) }
						N("installing?type="+Y);
						r(Z);
						return
					}
					else{ var ao="javascript:unityObject.doJavaInstall('"+Z+"');" }
				}
				else{
					if(t&&p.co){
						var ao=U+"3.0/co/UnityWebPlayer.application?installer="+encodeURIComponent(U+J());
						Y="clickonce"
					}
					else{
						if(p.win){ var ao=U+J() }
						else{
							if(P.platform=="MacIntel"){ var ao=U+"webplayer-i386.dmg" }
							else{
								if(P.platform=="MacPPC"){ var ao=U+"webplayer-ppc.dmg" }
								else{ var ao='javascript:window.open("http://unity3d.com/webplayer/");' }
							}
						}
					}
				}
				if(aq){
					var ap="Unity Web Player. Install now! Restart your browser after install.";
					var ai="http://webplayer.unity3d.com/installation/getunityrestart.png";
					var am=190;
					var aa=75
				}
				else{
					var ap="Unity Web Player. Install now!";
					var ai="http://webplayer.unity3d.com/installation/getunity.png";
					var am=193;
					var aa=63
				}
				var aj=ab.width||am;
				var ac=ab.height||aa;
				var an=y(-parseInt(aa/2));
				var af="unityObject.trackStatus('installing?type="+Y+"');";
				if(p.win&&p.ie){
					var ak='<img alt="'+ap+'" src="'+ai+'" width="'+am+'" height="'+aa+'" style="border-width: 0px;" />';
					var au='<a href="'+ao+'" title="'+ap+'" onclick="'+af+'"';
					if(W){
						au+=' style="display: block; height: '+y(aa)+"; position: relative; top: "+an+';"'
					}
					au+=">"+ak+"</a>";
					if(W){
						var ar='<div style="width: '+y(am)+'; margin: auto; position: relative; top: 50%;">'+au+"</div>";
						av.outerHTML='<div id="'+Z+'" style="width: '+y(aj)+"; height: "+y(ac)+'; text-align: center;">'+ar+"</div>"
					}
					else{ av.outerHTML='<div id="'+Z+'">'+au+"</div>" }
				}
				else{
					var aw=S.createElement("div");
					aw.setAttribute("id",Z);
					if(W){
						aw.style.width=y(aj);
						aw.style.height=y(ac);
						var ag=S.createElement("div");
						ag.style.width=y(am);
						ag.style.margin="auto";
						ag.style.position="relative";
						ag.style.top="50%"
					}
					var ad=S.createElement("a");
					ad.setAttribute("href",ao);
					ad.setAttribute("title",ap);
					ad.setAttribute("onclick",af);
					if(W){
						ad.style.display="block";
						ad.style.height=y(aa);
						ad.style.position="relative";
						ad.style.top=an
					}
					var X=S.createElement("img");
					X.setAttribute("alt",ap);
					X.setAttribute("src",ai);
					X.setAttribute("width",am);
					X.setAttribute("height",aa);
					X.style.borderWidth="0px";
					ad.appendChild(X);
					if(W){
						ag.appendChild(ad);
						aw.appendChild(ag)
					}
					else{
						aw.appendChild(ad)
					}
					av.parentNode.replaceChild(aw,av)
				}
				O(Z,true)
			}
			if(ah){
				ah({success:false,id:Z})
			}
			if(g&&t&&p.co&&!A){
				A=true;
				v(
				  function(){
					S.location=U+"3.0/co/UnityWebPlayer.application?installer="+encodeURIComponent(U+J())
				  }
				)
			}
			if(av&&!aq){
				(
				 function(){
					var ae=arguments.callee;
					C(
					  function(at){
						if(at=="installed"){
							O(Z,false);
							n(Z,ab,al,ah)
						}
						else{
							setTimeout(ae,500)
						}
					  }
					)
				 }
				)
				()
			}
		}
		function r(Y){
			var Z=S.getElementById(Y);
			var af=x[Y];
			var ab={
				id:Y,
				type:"application/x-java-applet",
				archive:U+"3.0/jws/UnityWebPlayer.jar",
				code:"UnityWebPlayer",
				width:af.attributes.width||600,
				height:af.attributes.height||450,
				name:"Unity Web Player"
			};
			var ad={
				context:Y,
				jnlp_href:U+"3.0/jws/UnityWebPlayer.jnlp",
				classloader_cache:false,
				installer:U+J(),
				image:"http://webplayer.unity3d.com/installation/unitylogo.png",
				centerimage:true,
				boxborder:false,
				scriptable:true,
				mayscript:true
			};
			for(var X in af.params){
				if(af.params[X]!=Object.prototype[X]){
					ad[X]=af.params[X];
					if(X.toLowerCase()=="logoimage"){
						ad.image=af.params[X]
					}
					else{
						if(X.toLowerCase()=="backgroundcolor"){
							ad.boxbgcolor="#"+af.params[X]
						}
						else{
							if(X.toLowerCase()=="bordercolor"){
								ad.boxborder=true
							}
							else{
								if(X.toLowerCase()=="textcolor"){
									ad.boxfgcolor="#"+af.params[X]
								}
							}
						}
					}
				}
			}
			if(p.win&&p.ie){
				var ag="";
				for(var X in ab){ag+=" "+X+'="'+ab[X]+'"'}
				var ac="";
				for(var X in ad){ac+='<param name="'+X+'" value="'+ad[X]+'" />'}
				Z.outerHTML="<object"+ag+">"+ac+"</object>"
			}
			else{
				var aa=S.createElement("object");
				for(var X in ab){
					aa.setAttribute(X,ab[X])
				}
				for(var X in ad){
					var ae=S.createElement("param");
					ae.name=X;
					ae.value=ad[X];
					aa.appendChild(ae)
				}
				Z.parentNode.replaceChild(aa,Z)
			}
			l=true;
			O(Y,true)
		}
		function o(Y,X){
			C(
			  function(Z){
				applet=x[Y];
				if(Z=="installed"){
					n(Y,applet.attributes,applet.params,applet.callback)
				}
				else{
					m(Y,applet.attributes,applet.params,applet.callback,applet.broken)
				}
			  }
			)
		}
		function I(Z){
			var Y=S.getElementById(Z);
			if(Y){
				if(p.win&&p.ie){
					var X=Y.firstChild;
					if(X&&X.nodeName=="OBJECT"){
						Y.style.display="none";
						(function(){
							if(X.readyState==4){
								for(var aa in X){
									if(typeof X[aa]=="function"){
										X[aa]=null
									}
								}
								Y.parentNode.removeChild(Y)
							}
							else{
								setTimeout(arguments.callee,10)
							}
						 }
						)
						();
						return
					}
				}
				Y.parentNode.removeChild(Y)
			}
		}
		function f(ab,ac){
			var Y=S.getElementById(ab);
			if(!Y){
				if(ac){
					ac(null)
				}
				return null
			}
			var X;
			if(p.win&&p.ie){
				var Z=Y.getElementsByTagName("object")[0];
				if(Z&&Z.nodeName=="OBJECT"){
					X=Z
				}
			}
			else{
				var aa=Y.getElementsByTagName("embed")[0];
				if(aa&&aa.nodeName=="EMBED"){
					X=aa
				}
			}
			return(
				   function(){
						if(X&&typeof X.GetPluginVersion=="undefined"){
							if(ac){
								setTimeout(arguments.callee,10)
							}
							return null
						}
						if(ac){
							ac(X)
						}
						return X
					}
				   )
			()
		}
		function O(Z,X){
			if(!E){
				return
			}
			var Y=X?"visible":"hidden";
			if(B&&S.getElementById(Z)){
				S.getElementById(Z).style.visibility=Y
			}
			else{
				i("#"+Z,"visibility: "+Y+";")
			}
		}
		function i(ac,Y,ab,ad){
			if(p.mac&&p.ie){
				return
			}
			var Z=S.getElementsByTagName("head")[0];
			if(!Z){
				return
			}
			var X=(ab&&typeof ab=="string")?ab:"screen";
			if(ad){
				h=null;
				k=null
			}
			if(!h||k!=X){
				var aa=S.createElement("style");
				aa.setAttribute("type","text/css");
				aa.setAttribute("media",X);
				h=Z.appendChild(aa);
				if(p.win&&p.ie&&typeof S.styleSheets!="undefined"&&S.styleSheets.length>0){
					h=S.styleSheets[S.styleSheets.length-1]
				}
				k=X
			}
			if(p.win&&p.ie&&typeof h.addRule=="object"){
				h.addRule(ac,Y)
			}
			else{
				if(h&&typeof S.createTextNode!="undefined"){
					h.appendChild(S.createTextNode(ac+" { "+Y+" }"))
				}
			}
		}
		var T=function(){
			if(p.win&&p.ie){
				if(typeof j.attachEvent!="undefined"){
					u(j,"onunload",e)
				}
				else{
					if(typeof j.onunload=="function"){
						var X=j.onunload;
						j.onunload=function(){
							X();
							e()
						}
					}
					else{
						j.onunload=e
					}
				}
			}
		}();
		function e(){
			for(var Y in V){
				var X=V[Y];
				X.target.detachEvent(X.type,X.event)
			}
			for(var Y in F){
				I(F[Y])
			}
			for(var Y in p){
				p[Y]=null
			}
			p=null;
			for(var Y in unityObject){
				unityObject[Y]=null
			}
			unityObject=null
		}
		function D(aa,Y,X){
			var Z={};
			if(aa&&typeof aa=="object"){
				for(var ab in aa){
					Z[ab]=aa[ab]
				}
			}
			Z.width=Y;
			Z.height=X;
			return Z
		}
		function c(X,ab){
			var aa="unityObject.firstFrameCallback();";
			var Z={};
			if(X&&typeof X=="object"){
				for(var Y in X){
					Z[Y]=X[Y];
					if(Y.toLowerCase()=="firstframecallback"){
						Z[Y]=aa+Z[Y];
						aa=null
					}
				}
			}
			if(aa){
				Z.firstFrameCallback=aa
			}
			Z.src=ab;
			return Z
		}
		return{
			embedUnity:function(ab,ad,Z,Y,X,ac,aa){
				if(p.w3&&!(p.wk&&p.wk<312)&&ab&&ad&&Z&&Y){
					b(
					  function(){
							var af=D(ac,Z,Y);
							var ae=c(X,ad);
							C(
								function(ag){
									if(ag=="installed"){
										n(ab,af,ae,aa)
									}
									else{
										m(ab,af,ae,aa,ag=="broken")
									}
								}
							)
					  }
					)
				}
				else{
					if(aa){
						aa({success:false,id:ab})
					}
				}
			},
			getObjectById:function(X,Y){
				if(p.w3&&X){
					return f(X,Y)
				}
				else{
					if(Y){
						Y(null)
					}
				}
				return null
			},
			setAutoHideShow:function(X){
				E=X
			},
			setFullSizeMissing:function(X){
				W=X
			},
			enableFullInstall:function(X){
				M=X
			},
			enableAutoInstall:function(X){
				g=X
			},
			enableJavaInstall:function(X){
				z=X
			},
			enableClickOnceInstall:function(X){
				t=X
			},
			enableGoogleAnalytics:function(X){
				w=X
			},
			addLoadEvent:v,
			addDomLoadEvent:b,
			ua:p,
			detectUnity:function(X){
				if(p.w3&&!(p.wk&&p.wk<312)&&X){
					C(X)
				}
				else{
					if(X){
						X("missing")
					}
				}
			},
			createUnity:function(Y,X,Z,aa){
				if(p.w3&&!(p.wk&&p.wk<312)&&Y&&X&&Z&&aa){
					n(Y,Z,X,aa)
				}
				else{
					if(aa){
						aa({success:false,id:Y})
					}
				}
			},
			removeUnity:function(X){
				if(p.w3){
					I(X)
				}
			},
			trackStatus:function(X){
				N(X)
			},
			doJavaInstall:function(X){
				r(X)
			},
			javaInstallDone:function(Y,X){
				setTimeout('unityObject.javaInstallDoneDirect("'+Y+'", '+X+");",0)
			},
			javaInstallDoneDirect:function(Y,X){
				o(Y,X)
			},
			firstFrameCallback:function(){
				d("running")
			}
		}
	}()
};


















