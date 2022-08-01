# 制作物の概要説明
 - ジャンル：アクション
 - 対象機器：パソコン、Android
 - ゲーム内容：あめ玉を転がして、ゴール代わりの容器にあめ玉を入れるとクリア
 - 操作方法<br>
&emsp;&emsp;&emsp;パソコンの場合、矢印キーで左右移動、スペースキーでジャンプ<br>
&emsp;&emsp;&emsp;Androidの場合、端末の傾きで左右移動、画面タップでジャンプ<br>
 - その他<br>
&emsp;&emsp;&emsp;最初はあめ玉を操作しますが、穴に落ちると操作する玉が目玉に変わり、<br>
&emsp;&emsp;&emsp;画面の半分がグレイスケールに変わります。<br>
&emsp;&emsp;&emsp;その状態で再度穴に落ちると画面全体がグレイスケールに変わります。<br>
 - アセット<br>
&emsp;&emsp;&emsp;Pixel Art Platformer - Village Props（地面と設置物）
&emsp;&emsp;&emsp;Sunny Land Forest（背景）
&emsp;&emsp;&emsp;FREE Ambient Instrumental Music（BGM）
&emsp;&emsp;&emsp;※あめ玉と目玉は自作
***
# 開発環境
 - ゲームエンジン：Unity
 - 開発言語：C#
 - 使用ツール
&emsp;&emsp;&emsp;Visual Studio 2019（コーディング）
&emsp;&emsp;&emsp;Github（ファイル管理）
***
# 制作意図
  ゲームエンジンを理解する為にUnityの物理演算やシェーダを使用した制作物を作成しました。
  あめ玉を穴に落とすと操作する玉が目玉に変わりますが、操作者の目玉をイメージしたため、
  視界に変化を発生させました。１つ目は左半分を２つ目は全体をグレイスケールにしました。
***
# 技術的課題や学んだこと
  視界に変化を与えるグレイスケールの実装方法とシェーダの切り替え方法が分からず、
  シェーダの記述方法を理解する為にUnity Documentationを読み、様々なブログを参考に
  する事で理解度が高まり、グレイスケールとシェーダの切り替えが実現できました。
  シェーダは、質感などでゲームへの没入感を高めて、ゲームの最適化に必要な技術なので
  とても楽しく学べました。
  