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
   
   [Embed(source="/_assets/assets.swf", symbol="rukia_fla.露琪雅_1")]
   public dynamic class 露琪雅_1 extends MovieClip
   {
       
      
      public var AImain:MovieClip;
      
      public var bdmn:MovieClip;
      
      public var bsatm:MovieClip;
      
      public var cbs1atm:MovieClip;
      
      public var cbsatm:MovieClip;
      
      public var k1atm:MovieClip;
      
      public var k2atm:MovieClip;
      
      public var k3atm:MovieClip;
      
      public var kj1atm:MovieClip;
      
      public var kj2atm:MovieClip;
      
      public var sbsatm:MovieClip;
      
      public var sh12atm:MovieClip;
      
      public var sh1atm:MovieClip;
      
      public var sh22atm:MovieClip;
      
      public var sh2atm:MovieClip;
      
      public var tkatm:MovieClip;
      
      public var tzatm:MovieClip;
      
      public var zh1atm:MovieClip;
      
      public var zh2atm:MovieClip;
      
      public var zh3atm:MovieClip;
      
      public function 露琪雅_1()
      {
         super();
         addFrameScript(13,this.frame14,21,this.frame22,24,this.frame25,28,this.frame29,37,this.frame38,42,this.frame43,75,this.frame76,85,this.frame86,86,this.frame87,99,this.frame100,111,this.frame112,115,this.frame116,132,this.frame133,150,this.frame151,156,this.frame157,162,this.frame163,168,this.frame169,178,this.frame179,179,this.frame180,183,this.frame184,193,this.frame194,199,this.frame200,210,this.frame211,215,this.frame216,219,this.frame220,222,this.frame223,235,this.frame236,239,this.frame240,251,this.frame252,256,this.frame257,260,this.frame261,261,this.frame262,269,this.frame270,275,this.frame276,280,this.frame281,302,this.frame303,305,this.frame306,316,this.frame317,323,this.frame324,328,this.frame329,337,this.frame338,340,this.frame341,348,this.frame349,367,this.frame368,385,this.frame386,402,this.frame403,403,this.frame404,407,this.frame408,416,this.frame417,440,this.frame441,444,this.frame445,469,this.frame470,480,this.frame481,494,this.frame495,510,this.frame511,514,this.frame515,521,this.frame522,529,this.frame530,534,this.frame535,546,this.frame547,591,this.frame592,592,this.frame593,598,this.frame599,625,this.frame626,636,this.frame637,677,this.frame678,684,this.frame685,690,this.frame691,699,this.frame700,728,this.frame729,766,this.frame767,776,this.frame777,789,this.frame790,790,this.frame791,800,this.frame801,805,this.frame806,822,this.frame823,905,this.frame906,921,this.frame922,996,this.frame997,1037,this.frame1038,1065,this.frame1066,1079,this.frame1080,1096,this.frame1097);
      }
      
      internal function frame14() : *
      {
         parent.$effect_ctrler.walk();
      }
      
      internal function frame22() : *
      {
         parent.$effect_ctrler.walk();
      }
      
      internal function frame25() : *
      {
         parent.$mc_ctrler.loop("走");
      }
      
      internal function frame29() : *
      {
         parent.$mc_ctrler.dash(3);
         parent.$mc_ctrler.setBisha();
         parent.$mc_ctrler.setBishaUP();
         parent.$mc_ctrler.setBishaSUPER();
         parent.$effect_ctrler.shadow(0,100,255);
         parent.$effect_ctrler.dash();
      }
      
      internal function frame38() : *
      {
         parent.$mc_ctrler.dashStop();
      }
      
      internal function frame43() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame76() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame86() : *
      {
         parent.$mc_ctrler.setTouchFloor("落地",true);
      }
      
      internal function frame87() : *
      {
         parent.$mc_ctrler.movePercent(1,2);
         parent.$effect_ctrler.shadow(0,100,255);
      }
      
      internal function frame100() : *
      {
         parent.$mc_ctrler.loop("跳砍loop");
      }
      
      internal function frame112() : *
      {
         parent.$mc_ctrler.addQi(5);
      }
      
      internal function frame116() : *
      {
         parent.$mc_ctrler.move(-5,-7);
         parent.$mc_ctrler.damping(0.5,0.5);
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame133() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame151() : *
      {
         parent.$mc_ctrler.movePercent(0.8,0);
         parent.$mc_ctrler.dampingPercent(0.08,0);
      }
      
      internal function frame157() : *
      {
         parent.$mc_ctrler.setAttack("砍2");
         parent.$mc_ctrler.setZhao1();
      }
      
      internal function frame163() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame169() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame179() : *
      {
      }
      
      internal function frame180() : *
      {
         parent.$mc_ctrler.movePercent(1.5,0);
         parent.$mc_ctrler.dampingPercent(0.2,0);
      }
      
      internal function frame184() : *
      {
         parent.$mc_ctrler.setAttack("砍3");
         parent.$mc_ctrler.setSkill2();
      }
      
      internal function frame194() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame200() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame211() : *
      {
         parent.$mc_ctrler.movePercent(2,0);
         parent.$mc_ctrler.dampingPercent(0.2,0);
      }
      
      internal function frame216() : *
      {
         parent.$mc_ctrler.setSkill1();
      }
      
      internal function frame220() : *
      {
         parent.$mc_ctrler.setBishaSUPER();
         parent.$mc_ctrler.setBisha();
      }
      
      internal function frame223() : *
      {
         parent.$mc_ctrler.setZhao1();
      }
      
      internal function frame236() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame240() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame252() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame257() : *
      {
         parent.$effect_ctrler.dash();
         parent.$effect_ctrler.slowDown(0.5);
      }
      
      internal function frame261() : *
      {
         parent.$fighter_ctrler.moveToTarget(30,0,true);
      }
      
      internal function frame262() : *
      {
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame270() : *
      {
         parent.$mc_ctrler.setZhao3();
         parent.$mc_ctrler.setBisha();
      }
      
      internal function frame276() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame281() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame303() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame306() : *
      {
         parent.$mc_ctrler.movePercent(1.5,-2);
         parent.$effect_ctrler.shadow(0,100,255);
      }
      
      internal function frame317() : *
      {
         parent.$mc_ctrler.setSkillAIR();
         parent.$mc_ctrler.setBishaAIR();
         parent.$mc_ctrler.dampingPercent(0.2,0.2);
         parent.$effect_ctrler.endShadow();
      }
      
      internal function frame324() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame329() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame338() : *
      {
         parent.$mc_ctrler.addQi(5);
      }
      
      internal function frame341() : *
      {
         parent.$effect_ctrler.shine(2798587);
      }
      
      internal function frame349() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame368() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame386() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame403() : *
      {
         parent.$effect_ctrler.shine();
      }
      
      internal function frame404() : *
      {
         parent.$effect_ctrler.shine(14731512);
      }
      
      internal function frame408() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame417() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame441() : *
      {
         parent.$mc_ctrler.addQi(5);
      }
      
      internal function frame445() : *
      {
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame470() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame481() : *
      {
         parent.$mc_ctrler.addQi(10);
      }
      
      internal function frame495() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame511() : *
      {
         parent.$mc_ctrler.addQi(20);
      }
      
      internal function frame515() : *
      {
         parent.$mc_ctrler.movePercent(-2,0);
         parent.$mc_ctrler.dampingPercent(0.2,0);
         parent.$effect_ctrler.shadow(0,100,255);
      }
      
      internal function frame522() : *
      {
         parent.$mc_ctrler.movePercent(2,0);
         parent.$mc_ctrler.dampingPercent(0.2,0);
      }
      
      internal function frame530() : *
      {
         parent.$effect_ctrler.endShadow();
      }
      
      internal function frame535() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame547() : *
      {
         parent.$effect_ctrler.bisha(false,"bsface1");
      }
      
      internal function frame592() : *
      {
         parent.$effect_ctrler.endBisha();
      }
      
      internal function frame593() : *
      {
         parent.$mc_ctrler.move(-10,0);
         parent.$mc_ctrler.damping(2,0);
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame599() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame626() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame637() : *
      {
         parent.$effect_ctrler.bisha(false,"bsface1");
      }
      
      internal function frame678() : *
      {
         parent.$mc_ctrler.move(-12,-14);
         parent.$mc_ctrler.damping(0.5,0.5);
         parent.$effect_ctrler.endBisha();
      }
      
      internal function frame685() : *
      {
         parent.$effect_ctrler.shine(2679032);
      }
      
      internal function frame691() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame700() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame729() : *
      {
         parent.$effect_ctrler.bisha(true,"bsface2");
      }
      
      internal function frame767() : *
      {
         parent.$effect_ctrler.endBisha();
         parent.$mc_ctrler.setSteelBody(true,true);
      }
      
      internal function frame777() : *
      {
         parent.$mc_ctrler.setSteelBody(false);
      }
      
      internal function frame790() : *
      {
         parent.$effect_ctrler.shine();
      }
      
      internal function frame791() : *
      {
         parent.$effect_ctrler.shine(11598072);
      }
      
      internal function frame801() : *
      {
         parent.$mc_ctrler.endAct();
      }
      
      internal function frame806() : *
      {
         parent.$effect_ctrler.shine();
      }
      
      internal function frame823() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame906() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame922() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame997() : *
      {
         parent.$mc_ctrler.idle();
      }
      
      internal function frame1038() : *
      {
         parent.$mc_ctrler.stop();
      }
      
      internal function frame1066() : *
      {
         parent.$mc_ctrler.stop();
      }
      
      internal function frame1080() : *
      {
         parent.$effect_ctrler.startWanKai("bsface2");
      }
      
      internal function frame1097() : *
      {
         parent.$fighter_ctrler.doWanKai();
      }
   }
}
