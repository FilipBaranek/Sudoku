   M S B A M L   `   `   `  ����A  =Sudoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Sudoku.ViewsSudoku.Views  5      !    Sudoku.Views.DifficultyView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Sudoku.Views     5      .��5
      �    
Background#���   6     � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��CHOOSE DIFFICULTY��5        DifficultyText#���  5      �� 5   
   $��1��6   $��Center=�5      $	��Top=�5       �� Orientation$ 
Horizontal=�5      .+�5      �� $
��EASY��6   
���2͚�  DifficultyButton#���  5      ��5      �� *EasyCommand+�� 5      $��MEDIUM��6   
��� ���#���  5      ��5       �� *MediumCommand+�� 5!      $
��HARD��6   
���  ��#���  5#      ��5$      �� *HardCommand+5%      5'      5(      