   M S B A M L   `   `   `  ����A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Sudoku.ViewsSudoku.Views  5          Sudoku.Views.TrainingView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      &localclr-namespace:Sudoku.Views  5        J� InputBindings  5      �� 5   
   
 �� Key$	 Esc��6    �� Command 6   �� *PauseManager.PauseTrigger+�� 5   
   $ H��6    6   �� *HintManager.HintTrigger+�� 5   
   $ D1��6    �� CommandParameter$ 1��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D2��6   $ 2��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D3��6   $ 3��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D4��6   $ 4��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D5��6   $ 5��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D6��6   $ 6��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D7��6   $ 7��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D8��6   $ 8��6K    6   �� *SelectNumberByKeyTrigger+6K   �� 5   
   $ D9��6   $ 9��6K    6   �� *SelectNumberByKeyTrigger+6K   .��5      �    
Background#���   6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��2* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   .+�5"   
   �� $��0��6   $��/Images/logo.png��6   �� 5$   
   $��1��6   $��Center=�5%      $��Center=�5&      ��5'      �� *	GameCells+��5(      �� 5)      .R�5*      A�  A� Rows$ 9��6"    A� Columns$ 9��6+   5+      ��5.      �� 5/      .R�50      ��   
GameButton#���  6   ��51      �� *	Command+��52      �� *	Parameter+��53      �� *
Background+��54      �� *BorderThickness+��55      �� *	Content+��56      �� *
FontSize+��57      �� *
Foreground+��58      �� *	Alignment+  59      d� 5:       d� Gesture$ 
RightClicka�6+    5;   +   �� *RightClickCommand+ 5<   +   �� *	Parameter+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5>      +  %Microsoft.Xaml.Behaviors.EventTrigger  5?      	   	EventName$	 
MouseEnter��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 5@   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *MouseOverCommand+   CommandParameter 5A   8   �� *	Parameter+5B      5D      5E      5G      �� 5I   
   $��2��6   $��Center=�5J      $��Center=�5K      ��5L      �� *PivotElements+��5M      �� 5N      .R�5O      ��  �� Orientation$ 
Horizontal=�6!   5P      ��5S      �� 5T      .R�5U      	�   PivotButton#���  6"    	� 	GroupName$ 
PivotGroup��5V   "   ��5W   "   �� *	Command+��5X   "   �� *Value+��5Y   "   �� *	Content+ d� 	IsChecked 5Z   "   �� *	IsChecked+5[      5]      �� 5_   
   $��3��6   $��Center=�5`        
HintButton#���  5a      $
��Hint��5b      ��5c      �� *HintManager.HintTrigger+�� 5e   
   $��1��6     HintMessageBorder#���  5f      y�5g      �� *HintManager.MessageVisible+._�5h      � .+�5i      ��   HintMessage#���  6   ��5j      �� *HintManager.Message+�� 5k        CloseButton#���  6   ��5l      �� *HintManager.VisibilityTrigger+5m      5n      � 5p   
   $��4��6   ���	#CC000000y�5r      �� *PauseManager.Visible+.+�5t      �� $��Center=�6   $��Center=�5u      $ Vertical=�5v      .+�5w      ��   	PauseText#���  6   $
��MENU��5x      �� 5y        PauseButton#���  6   $��Resume��5z      ��5{      �� *PauseManager.PauseTrigger+�� 5|      #���  6   $��Rules��5}      ��5~      �� *PauseManager.Rules+�� 5      #���  6   $��Hint options��5�      ��5�      �� *PauseManager.HintOptions+�� 5�      #���  6   $��Back to menu��5�      ��5�      �� *PauseManager.BackToMenu+5�      5�      � 5�   
   $��4��6   ���	#CC000000y�5�      �� *PauseManager.HintsVisible+.+�5�      ��  	 HintMenuLogo#��� 	 6   $��/Images/logo.png��5�      �� 5�      $��Center=�6   $��Center=�5�      $ Vertical=�5�      .+�5�      ��  
 HintOptionsHeader#��� 
 6   $��CHECK GAMEBOARD STYLE��5�      $��20q�5�      �� 5�      $��Center=�6   .+�5�      ��   CheckBox#���  6   $��Automatic candidates��5�       5�      �� *ToggleCandidates+ �� Mode$ TwoWay=��� 5�      #���  6   $��Mark selected numbers��5�       5�      �� *ToggleMarkNumbers+$ TwoWay=��� 5�      #���  6   $��	Crosshair��5�       5�      �� *ToggleCrosshair+$ TwoWay=�5�      �� 5�      $ Vertical=�6   $��60q�5�      .+�5�      �� #��� 
 6    $��SELECT HINT TYPE��5�       �� 5�        
Algorithms#���  6    �� DisplayMemberPath$
 Name��5�      ��5�      �� *HintManager.HintTypes+ �� SelectedItem 5�      �� *HintManager.SelectedHint+5�      5�      �� 5�      #���  6   $��Bottom=�5�      $	��100q�5�      $
��Back��5�      ��5�      �� *PauseManager.HintOptions+5�      5�      5�      