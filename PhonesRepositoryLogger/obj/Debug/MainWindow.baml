   M S B A M L   `   `   `  ����Q  MPhonesRepositoryLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PhonesRepositoryLoggerPhonesRepositoryLogger  5   	   '    !PhonesRepositoryLogger.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:PhonesRepositoryLogger     5   	     � Title$  Phones��5   	   $	��350��6   $	��800��6%   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��0.8* �6   .+�5   
   �� $��0��6   ��6"   �� *
PhonesList+ �� SelectedItem 6E   �� *SelectedPhone+��5      �� 5      .R�5      �� $��5q�6!   .+�5      �� $��18(�6$   ��62   ��  �� Path$ Title��� 5      ��6$   �� $ Company��� 5      ��6$   �� $ Price�5      5      5      �� 5   
   $��1��6    � DataContext 6%   �� *SelectedPhone+.+�5      �� $��16(�6   $
��Bold"�6&   $'��!Выбранный элемент��68   �� 5      $
��Bold"�6   $��Модель��6*   �� 5      ��6   �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $
��Bold"�6   $ ��Производитель��6*   �� 5       ��6   �� *	Company+$ PropertyChanged=��� 5!      $
��Bold"�6   $��Цена��6*   �� 5"      -   5        $��
PhonePrice��5"      ��6(   �� *Price+�� 5#      -   5        $��Изменить��5#      6(   5$      5%      5&      