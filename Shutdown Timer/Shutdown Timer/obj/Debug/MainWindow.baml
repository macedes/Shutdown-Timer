   M S B A M L   `   `   `  ����  Shutdown Timer    Shutdown_Timer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Shutdown Timer��5   	   $	��250��6    $	��300��6-    � 
ResizeMode$ CanMinimize=�69   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $	��110 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $	��100 �6   .+�5   
   �� -   5         ��Name$ 	TimeLabel��5      $��2��6#   $��Center=�67   $��center=�6R   $
��Bold"�6o   $�� ��6�   $��20(�6�   � 5   
   -   5        $��ProgressBar��5   S   $
��0,10q�6   $��1��6$   $��2��61   $��15��6F   6S   �� 5   
   -   5         ��Name$ TimerButton��5      $��2��6&   $��50��63   $��Timer Starten��6?   6W   �� 5   
   $��30,0,0,0q�6   $��2��6'   $��1��64   .+�5      �� $
��Bold"�6   .��6&   	Dauer :6/   	� 5      -   5        $��radio30��5       d� 	IsChecked$
 TrueY�6)   .��6:   30 min6B   	� 5      -   5        $��radio60��5      .��6)   60 min61   	� 5      -   5        $��radio90��5      .��6)   90 min61   5      5      5      