   M S B A M L   `   `   `  ����A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Sudoku.ViewsSudoku.Views  5          Sudoku.Views.GameView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Sudoku.Views     5        J� InputBindings  5
      �� 5   
   
 �� Key$	 Esc��6    �� Command 6   �� *PauseManager.PauseTrigger+�� 5   
   $ D1��6    �� CommandParameter$ 1��6?    6   �� *SelectNumber+6?   �� 5   
   $ D2��6   $ 2��6?    6   �� *SelectNumber+6?   �� 5   
   $ D3��6   $ 3��6?    6   �� *SelectNumber+6?   �� 5   
   $ D4��6   $ 4��6?    6   �� *SelectNumber+6?   �� 5   
   $ D5��6   $ 5��6?    6   �� *SelectNumber+6?   �� 5   
   $ D6��6   $ 6��6?    6   �� *SelectNumber+6?   �� 5   
   $ D7��6   $ 7��6?    6   �� *SelectNumber+6?   �� 5   
   $ D8��6   $ 8��6?    6   �� *SelectNumber+6?   �� 5   
   $ D9��6   $ 9��6?    6   �� *SelectNumber+6?   .��5      �    
Background#���   6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��2* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   .+�5    
   �� $��0��6   $��/Images/logo.png��6   �� 5"   
   $��1��6   $��Center=�5#      $��Center=�5$      ��5%      �� *	GameCells+��5&      �� 5'      .R�5(      A�  A� Rows$ 9��6"    A� Columns$ 9��6+   5)      ��5,      �� 5-      .R�5.      ��   
GameButton#���  6   ��5/      �� *	Command+��50      �� *	Parameter+��51      �� *
Background+��52      �� *BorderThickness+��53      �� *	Content+54      56      �� 58   
   $��2��6   $��Center=�59      $��Center=�5:      ��5;      �� *PivotElements+��5<      �� 5=      .R�5>      ��  �� Orientation$ 
Horizontal=�6!   5?      ��5B      �� 5C      .R�5D      	�   PivotButton#���  6"    	� 	GroupName$ 
PivotGroup��5E   "   ��5F   "   �� *	Command+��5G   "   �� *Value+��5H   "   �� *	Content+	 d� 	IsChecked	 5I   "   �� *	IsChecked+5J      5L      �� 5N   
   $��3��6   $ Vertical=�5O      $��Center=�5P      $	��Top=�5Q      $��3q�5R      .+�5S      �� $ 
Horizontal=�6   $��Center=�5T      .+�5U      ��   GameText#���  6   $��Time left: ��5V      �� 5W      #���  6   ��5X      �� *
TimeLeft+5Y      �� 5Z      $ 
Horizontal=�6   $��Center=�5[      .+�5\      �� #���  6   $��Incorrect left: ��5]      �� 5^      #���  6   ��5_      �� *Game.Incorrect+5`      5a      � 5c   
   $��4��6   ���	#CC000000y�5e      �� *PauseManager.Visible+.+�5f      �� $��Center=�6   $��Center=�5g      $ Vertical=�5h      .+�5i      ��   	PauseText#���  6   $��
PAUSE MENU��5j      �� 5k        PauseButton#���  6   $��Resume��5l      ��5m      �� *PauseManager.PauseTrigger+�� 5n      #���  6   $��Rules��5o      ��5p      �� *PauseManager.Rules+�� 5q      #���  6   $��Back to menu��5r      ��5s      �� *PauseManager.BackToMenu+5t      5u      5w      5y      