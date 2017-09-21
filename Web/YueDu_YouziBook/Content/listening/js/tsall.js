var url,bid,cid,mediaURL,nowTime,recordTime,starttime,dat,currTime;
var playx = 0;
var jgTime = 10;

//播放器控制面板	
var Control = function(o){
	this.setting         = (typeof o === 'object')? o : {};		
	this.audio           = this.setting.audio;
	this.progressWrap    = this.setting.progressWrap;
	this.playBtn         = this.setting.playBtn;
	this.progress        = this.setting.progress;
	this.allTimeNode     = this.setting.allTimeNode;	  
	this.currentTimeNode = this.setting.currentTimeNode;  
	this.init();
}

Control.prototype={
//初始化
	init:function(){
		//播放控制	
		this.start = true;
		//定时器
		this.timer = null;					
		//可选播放模式

		//默认播放模式
		this.ModeIndex = 0;
	},
	

	//主控
	mainControl:function(){
		function pp(a){
				if(a.start){
					a.goPlay();
				}else{
					a.goPause();
				}
		}
		setTimeout(pp(this),500);
		
	},
	
	
	//播放进度选择
	selectTime:function(event){
		var moveTo = event.pageX - this.progressWrap.offset().left;
		this.audio.currentTime = moveTo/parseInt(this.progressWrap.css("width"))*this.audio.duration;
		this.progress.css("width",moveTo+"px");
	},
	
	//自动播放
	autoPlay:function(){
		//监听歌曲结束
		var that = this;

		this.audio.addEventListener('waiting', function () {
			$(".pop").html("加载中...")

		},false);
		
		this.audio.addEventListener('ended', function () {
		if(document.getElementById("progress").offsetWidth==198){
			var nextUrl = $("#nextButton").attr("href");
			if(nextUrl){
				window.location.href=nextUrl;
			}
		}		

		},false);
	},
	

	//转换为时间格式
	timeDispose:function (number) {
		var minute = parseInt(number / 60);
		var second = parseInt(number % 60);
		minute = minute >= 10 ? minute : "0" + minute;
		second = second >= 10 ? second : "0" + second;
		return minute + ":" + second;
	},	
	
	//播放时间
	oTime:function(){
		$('#currentTime').html(this.audio.readyState);
		if(this.audio.readyState >=4){

			var currentProgress = Math.round(this.audio.currentTime/this.audio.duration*parseInt(this.progressWrap.css("width")));
			this.progress.css("width",currentProgress+"px");
			this.allTimeNode.html(this.timeDispose(this.audio.duration));
			this.currentTimeNode.html(this.timeDispose(this.audio.currentTime));

			if(this.audio.currentTime>0){
				url = "./css/img/johann_sebastian_bach_air.mp3";
				bid = $("input[name='bid']").val();
				cid = $("input[name='cid']").val();
				mediaURL = $("input[name='mediaURL']").val();
				nowTime = new Date();
				recordTime = nowTime.getFullYear()+""+(nowTime.getMonth()>8?'':'0')+(nowTime.getMonth()+1)+""+(nowTime.getDate()>9?'':'0')+nowTime.getDate()+(nowTime.getHours()>9?'':'0')+nowTime.getHours()+(nowTime.getMinutes()>9?'':'0')+nowTime.getMinutes()+(nowTime.getSeconds()>9?'':'0')+nowTime.getSeconds();
				currTime = Math.ceil(this.audio.currentTime);

					

				dat="recordTime="+ recordTime +"&accessType=1&playtype=1&bid="+bid+"&cid="+cid+"&starttime="+starttime+"&playtime=10&sourcetype=19&mediaURL="+mediaURL;
				if(playx%jgTime==0){

					$.ajax({
						url:url,
						type:"POST",
						data:dat,
						success: function(data){
							console.log("success");
						}
					});
				}
				playx++;
			}
			if(this.audio.currentTime>1){
				$(".pop").hide();
			}
             //lyf 20150505 新增判断
            if(this.audio.currentTime == this.audio.duration){
				this.playBtn.addClass("play");
				this.playBtn.removeClass("pause");	
				$(".cdBox .coverBox").removeClass("spin");
				$(".cdBox .gan").removeClass("origin");
			}
		}
	},
	
	//播放
	goPlay:function(){
		//if(this.songReady()){		

			this.audio.play();
			var _this = this;
			this.goPlayStyle();
			this.timer = setInterval(function(){_this.oTime()},1000)
			this.start = false;
			this.autoPlay();
		thisTime = new Date();
		starttime= thisTime.getFullYear()+""+(thisTime.getMonth()>8?'':'0')+(thisTime.getMonth()+1)+""+(thisTime.getDate()>9?'':'0')+thisTime.getDate()+(thisTime.getHours()>9?'':'0')+thisTime.getHours()+(thisTime.getMinutes()>9?'':'0')+thisTime.getMinutes()+(thisTime.getSeconds()>9?'':'0')+thisTime.getSeconds();
		//}
		
		
	},
	
	//暂停
	goPause:function(){
		
			this.audio.pause();
			this.goPauseStyle();
			clearInterval(this.timer);
			this.start = true;
		
		
	},
	
	//播放样式
	goPlayStyle:function(){
		var $oLiIndex = $(this.oLi);
		$(".frmPause").removeClass("frmPause");
			
		this.playBtn.addClass("pause");
		this.playBtn.removeClass("play");
		$(".cdBox .coverBox").addClass("spin");
		$(".cdBox .gan").addClass("origin");
		$("#progress").show();
	
	},
	
	//暂停样式
	goPauseStyle:function(){
		var $oLiIndex = $(this.oLi);
		$(".frmPause").removeClass("frmPause");
		this.playBtn.addClass("play");
		this.playBtn.removeClass("pause");
		$(".cdBox .coverBox").removeClass("spin");
		$(".cdBox .gan").removeClass("origin");	
	}			
}

//实例化控制台
var myControl = new Control({
			 audio : document.getElementById("myMusic"), //播放器
	  playModeNode : $("#modeButton"),	 //模式选择按钮
		   playBtn : $("#playButton"),   //主控按钮
	  progressWrap : $("#progressWrap"), //歌曲进度条容器
		  progress : $("#progress"),     //歌曲进度条
	   allTimeNode : $("#totleTime"),    //当前时间容器
   currentTimeNode : $("#currentTime")   //当前时间容器
});	

//$(".pop").html("请点播放键播放");
//myControl.goPlay();