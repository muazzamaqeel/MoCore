   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   E AMoCore 1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:MoCore_1_0.Views.ConcertClasses_Views%MoCore_1_0.Views.ConcertClasses_Views 5   	   D   >MoCore_1_0.Views.ConcertClasses_Views.PerformanceMonitorWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   Elocal3clr-namespace:MoCore_1_0.Views.ConcertClasses_Views     5   	     � Title$  Performance Monitor��5   	   $	��600��6%   $
��1000��62   ���#1E1E2F � WindowStartupLocation$ CenterScreen=�5       � WindowStyle$ SingleBorderWindow=�5   	    � AllowsTransparency$ False��6*    � 
ResizeMode$ CanResizeWithGrip=�5	   	   .��5      ��  �� CornerRadius$ 10��6   ���#1E1E2F �� Padding$ 10q�64   ���#3C3F52$��1q�6W   ._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      � -   5        ���#292B38$��40��5   (   $	��Top=�64   .+�5      �� $��Performance Monitor��6   $��Center=�67   
�������$��16(�6e   $��10,0,0,0q�6s   �� 5      -   5        $	��✕��5      $��30��6%   $��30��60   $��Right=�6<   $��Center=�6X   
������ 
�������$��0q�5   E   5      5      �� 5      $��1��6   ���#1E1E2F$��0q�6;   .@�5      �� $	��CPU��6   .��5      �� ���#292B38$ 10��62   ._�5      �� .+�5       �� $��	CPU Usage��6(   
�������$��16(�6L   $��10q�6Z   $��Center=�6f   #   LiveCharts.Wpf.CartesianChart  5!      -   5          Name$ CpuChart��5!   1   $��10q�6C   ���#1E1E2F$	��409��6}   &    LiveCharts.Wpf.Charts.Base.Chart	   Series	 5#   &   !   LiveCharts.Wpf.ColumnSeries     LiveCharts.Wpf.Series
   Title$
 	CPU Usage��67      Values 6I   �� *
CpuUsage+   AxisX 5&   &      LiveCharts.Wpf.Axis     Title$
 Core��6/      Labels 6<   �� *
CpuCores+   AxisY 5)   &     $ 	Usage (%)��6/   5+      5,      5-      5.      �� 51      $	��GPU��6   .��52      �� ���#292B38$ 10��62   ._�53      �� .+�54      �� $��	GPU Usage��6(   
�������$��16(�6L   $��10q�6Z   $��Center=�6f     55      -   5        $ GpuChart��55   1   $��10q�6C   ���#1E1E2F$	��414��6d   	 57   &     $
 	GPU Usage��67    6I   �� *
GpuUsage+ 5:   &     $	 GPU��6/    6;   �� *
GpuCores+ 5=   &     $ 	Usage (%)��6/   5?      5@      5A      5B      �� 5E      $��Background Resources��6   .��5F      �� ���#292B38$ 10��62   ._�5G      �  5H      �� 5I   "   $��* �60   �� 5J   "   $
��Auto �60   .+�5L      &   System.Windows.Controls.DataGrid  -   5          Name$ BackgroundResourcesGrid��5L   '   $��10q�5M   '   ���#E8E8E8
���   ����#CCCCCC$��1q�5Q   '   $��0��5R   '      AutoGenerateColumns$ False��5S   '      
IsReadOnly$
 True��5T   '      RowBackground
 �����   AlternatingRowBackground �#F3F3F3 � 	Resources 5W   "   *  $System.Windows.Controls.DataGridCell �� 
TargetType �� Property �� Value��%S   '        �� 5X   &   "  6,   .�5Y   *   ��  � ��$	 5,2q�6D   5Z   '   5[   #      Columns 5\   "   0  *System.Windows.Controls.DataGridTextColumn  5]   &   ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Process Name��69    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ *
 5_   9   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5^   9   �� *Name+5_   9     5`   &   $ Memory Usage (MB)��69   $ *
 5b   9    5a   9   �� *MemoryUsage+5b   9   5d      �� 5e      -   5        $��Optimize��5e   %   $��Center=�68   $��10q�6U   $
��10,5q�6a   $��1��6p   6}   5f      5g      5h      5i      5j      5k      5l      