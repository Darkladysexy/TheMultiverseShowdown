package rukia_fla
{
   import flash.accessibility.*;
   import flash.display.*;
   import flash.errors.*;
   import flash.events.*;
   import flash.filters.*;
   import flash.geom.*;
   import flash.media.*;
   import flash.net.*;
   import flash.net.drm.*;
   import flash.system.*;
   import flash.text.*;
   import flash.text.ime.*;
   import flash.ui.*;
   import flash.utils.*;
   
   [Embed(source="/_assets/assets.swf", symbol="rukia_fla.露琪雅2_8")]
   public dynamic class 露琪雅2_8 extends MovieClip
   {
       
      
      public var AImain:MovieClip;
      
      public var bdmn:MovieClip;
      
      public var bsatm:MovieClip;
      
      public var cbs1atm:MovieClip;
      
      public var cbs2atm:MovieClip;
      
      public var cbsatm:MovieClip;
      
      public var k1atm:MovieClip;
      
      public var k2atm:MovieClip;
      
      public var k3atm:MovieClip;
      
      public var k4atm:MovieClip;
      
      public var kj11atm:MovieClip;
      
      public var kj1atm:MovieClip;
      
      public var kj21atm:MovieClip;
      
      public var kj2atm:MovieClip;
      
      public var sbsatm:MovieClip;
      
      public var sh12atm:MovieClip;
      
      public var sh1atm:MovieClip;
      
      public var sh21atm:MovieClip;
      
      public var sh22atm:MovieClip;
      
      public var sh2atm:MovieClip;
      
      public var tkatm:MovieClip;
      
      public var tzatm:MovieClip;
      
      public var zh1atm:MovieClip;
      
      public var zh2atm:MovieClip;
      
      public var zh31atm:MovieClip;
      
      public var zh3atm:MovieClip;
      
      public function 露琪雅2_8()
      {
         super();
         addFrameScript(11,this.frame12,19,this.frame20,26,this.frame27,33,this.frame34,44,this.frame45,47,this.frame48,77,this.frame78,89,this.frame90,94,this.frame95,99,this.frame100,111,this.frame112,121,this.frame122,137,this.frame138,150,this.frame151,159,this.frame160,170,this.frame171,181,this.frame182,189,this.frame190,196,this.frame197,213,this.frame214,221,this.frame222,226,this.frame227,231,this.frame232,248,this.frame249,258,this.frame259,264,this.frame265,271,this.frame272,292,this.frame293,295,this.frame296,307,this.frame308,316,this.frame317,323,this.frame324,334,this.frame335,353,this.frame354,355,this.frame356,360,this.frame361,374,this.frame375,382,this.frame383,389,this.frame390,399,this.frame400,406,this.frame407,423,this.frame424,427,this.frame428,442,this.frame443,450,this.frame451,456,this.frame457,458,this.frame459,463,this.frame464,478,this.frame479,492,this.frame493,498,this.frame499,503,this.frame504,509,this.frame510,524,this.frame525,532,this.frame533,544,this.frame545,557,this.frame558,577,this.frame578,596,this.frame597,640,this.frame641,641,this.frame642,644,this.frame645,645,this.frame646,660,this.frame661,672,this.frame673,683,this.frame684,730,this.frame731,738,this.frame739,755,this.frame756,771,this.frame772,800,this.frame801,867,this.frame868,875,this.frame876,876,this.frame877,894,this.frame895,910,this.frame911,997,this.frame998,1001,this.frame1002,1004,this.frame1005,1020,this.frame1021,1084,this.frame1085,1095,this.frame1096,1136,this.frame1137,1164,this.frame1165);
      }
      
      internal function frame12() : *
      {
         parent.$effect_ctrler.walk();
      }
      
      internal function frame20() : *
      {
         parent.$effect_ctrler.walk();
      }
      
      internal function frame27() : *
      {
         parent.$mc_ctrler.loop("走");
      }
      
      internal function frame34() : *
      {
         parent.$mc_ctrler.dash(2.5);
         parent.$mc_ctrler.setBisha();
         parent.$mc_ctrler.setBishaUP();
         parent.$mc_ctrler.setBishaSUPER();
         parent.$effect_ctrler.shadow(0,100,255);
         parent.$effect_ctrler.dash();
      }
      
      internal function frame45() : *
      {
         parent.$mc_ctrler.dashStop();
      }
      
      internal function frame48() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame78() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame90() : *
      {
         parent.$mc_ctrler.movePercent(1.5,0);
         parent.$mc_ctrler.dampingPercent(0.1,0);
      }
      
      internal function frame95() : *
      {
         parent.$mc_ctrler.endAct();
         parent.$mc_ctrler.setSkillAIR("跳招");
         parent.$mc_ctrler.setTouchFloor("落地",true);
      }
      
      internal function frame100() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame112() : *
      {
         parent.$mc_ctrler.addQi(5);
         parent.$mc_ctrler.isApplyG(false);
         parent.$mc_ctrler.move(0,1);
      }
      
      internal function frame122() : *
      {
         parent.$mc_ctrler.fire("tzatm",{
            "x":{
               "start":10,
               "add":1,
               "max":30
            },
            "y":{
               "start":10,
               "add":1,
               "max":30
            },
            "hold":60,
            "hits":1,
            "hp":200
         });
         parent.$mc_ctrler.isApplyG(true);
         parent.$mc_ctrler.move(-5,-5);
         parent.$mc_ctrler.damping(0.2,0.2);
         parent.$effect_ctrler.shine(16711680);
      }
      
      internal function frame138() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame151() : *
      {
         parent.$mc_ctrler.movePercent(2,0);
         parent.$mc_ctrler.dampingPercent(0.2,0);
      }
      
      internal function frame160() : *
      {
         parent.$mc_ctrler.setAttack("砍2");
         parent.$mc_ctrler.setZhao1();
      }
      
      internal function frame171() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame182() : *
      {
         parent.$mc_ctrler.movePercent(0.5,0);
         parent.$mc_ctrler.dampingPercent(0.05,0);
      }
      
      internal function frame190() : *
      {
         parent.$mc_ctrler.setBishaSUPER();
         parent.$mc_ctrler.setSkill1();
         parent.$mc_ctrler.setAttack("砍3");
      }
      
      internal function frame197() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame214() : *
      {
         parent.$mc_ctrler.movePercent(1,0);
         parent.$mc_ctrler.dampingPercent(0.1,0);
      }
      
      internal function frame222() : *
      {
         parent.$mc_ctrler.setSkill1();
         parent.$mc_ctrler.setZhao3();
         parent.$mc_ctrler.setAttack("砍4");
      }
      
      internal function frame227() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame232() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame249() : *
      {
         parent.$mc_ctrler.movePercent(2,0);
         parent.$mc_ctrler.dampingPercent(0.1,0);
      }
      
      internal function frame259() : *
      {
         parent.$mc_ctrler.setBisha();
         parent.$mc_ctrler.setSkill2();
         parent.$mc_ctrler.setZhao1();
      }
      
      internal function frame265() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame272() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame293() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame296() : *
      {
         parent.$mc_ctrler.movePercent(1.3,0);
      }
      
      internal function frame308() : *
      {
         parent.$mc_ctrler.dampingPercent(0.1,0);
      }
      
      internal function frame317() : *
      {
         parent.$mc_ctrler.setBishaUP();
         parent.$mc_ctrler.setZhao3();
      }
      
      internal function frame324() : *
      {
         parent.$mc_ctrler.endAct();
         parent.$mc_ctrler.setDash();
      }
      
      internal function frame335() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame354() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame356() : *
      {
         parent.$effect_ctrler.dash();
      }
      
      internal function frame361() : *
      {
         parent.$fighter_ctrler.moveToTarget(50,0,true);
         parent.$effect_ctrler.slowDown(0.3);
      }
      
      internal function frame375() : *
      {
         parent.$mc_ctrler.setBishaSUPER();
         parent.$mc_ctrler.setBishaUP();
         parent.$mc_ctrler.setZhao3();
         parent.$mc_ctrler.setZhao1();
         parent.$mc_ctrler.setSkill1();
      }
      
      internal function frame383() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame390() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame400() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame407() : *
      {
         parent.$mc_ctrler.fire("zh1atm",{
            "x":{
               "start":10,
               "add":1,
               "max":30
            },
            "hits":1,
            "hp":200
         });
         parent.$effect_ctrler.shine(16711680);
      }
      
      internal function frame424() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame428() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame443() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame451() : *
      {
         parent.$mc_ctrler.movePercent(1.5,0);
         parent.$effect_ctrler.shadow(0,100,255);
      }
      
      internal function frame457() : *
      {
         parent.$mc_ctrler.dampingPercent(0.2,0);
         parent.$effect_ctrler.endShadow();
      }
      
      internal function frame459() : *
      {
         parent.$effect_ctrler.shine();
      }
      
      internal function frame464() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame479() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame493() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame499() : *
      {
         parent.$mc_ctrler.move(20,-20);
      }
      
      internal function frame504() : *
      {
         parent.$mc_ctrler.damping(2,2);
      }
      
      internal function frame510() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame525() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame533() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame545() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame558() : *
      {
         parent.$mc_ctrler.addQi(20);
      }
      
      internal function frame578() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame597() : *
      {
         parent.$effect_ctrler.bisha(false,"bsface2");
      }
      
      internal function frame641() : *
      {
         parent.$effect_ctrler.endBisha();
      }
      
      internal function frame642() : *
      {
         parent.$mc_ctrler.movePercent(-1,0);
      }
      
      internal function frame645() : *
      {
         parent.$mc_ctrler.dampingPercent(0.05,0);
      }
      
      internal function frame646() : *
      {
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame661() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame673() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame684() : *
      {
         parent.$effect_ctrler.bisha(false,"bsface2");
      }
      
      internal function frame731() : *
      {
         parent.$effect_ctrler.endBisha();
         parent.$mc_ctrler.setSteelBody(true,true);
      }
      
      internal function frame739() : *
      {
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame756() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame772() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame801() : *
      {
         parent.$effect_ctrler.bisha(true,"bsface2");
      }
      
      internal function frame868() : *
      {
         parent.$effect_ctrler.endBisha();
         parent.$mc_ctrler.setSteelBody(true);
      }
      
      internal function frame876() : *
      {
         parent.$effect_ctrler.shine(7393520);
      }
      
      internal function frame877() : *
      {
         parent.$effect_ctrler.shine();
      }
      
      internal function frame895() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame911() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame998() : *
      {
         parent.$mc_ctrler.movePercent(0.5,0);
      }
      
      internal function frame1002() : *
      {
         parent.$mc_ctrler.dampingPercent(0.2,0);
      }
      
      internal function frame1005() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame1021() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame1085() : *
      {
         parent.$effect_ctrler.endWanKai();
      }
      
      internal function frame1096() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame1137() : *
      {
         parent.$mc_ctrler.stop();
      }
      
      internal function frame1165() : *
      {
         parent.$mc_ctrler.stop();
      }
   }
}
