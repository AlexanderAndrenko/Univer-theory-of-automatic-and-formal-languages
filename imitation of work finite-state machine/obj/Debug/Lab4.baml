   M S B A M L   `   `   `  ����E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator  5          Translator.Lab4   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Translator     5        N� Title$
  Lab4��5	      .��5      � $��4q�6   
������� � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5       � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��2��6"   $��Center=�66   .+�5      �� $��Center=�6   $��Center=�65   $
��auto��6P   ��Программа преобразователь недетерминированного кончного автомата в детерминированный6�   5      �� 5      $��1��6   $��0��5       �� Orientation$ Vertical=�5      .+�5      �� $��Center=�6   $��Center=�5      $
��Wrap=�5      $��3q�5      WUПравила вводятся в форме AaB (A -> aB), Aa (A -> a), AE (A -> E)5      �� 5       $ 
Horizontal=�6   .+�5!      �� -   5        $��
ChainRules��5!      $	��350��5"      $
��auto��5#       �� TextWrapping$ WrapWithOverflow=�5$      $��3q�6>   .��6I   5%      �� 5&      -   5        $��30��5&      $��30��5'      $��3q�5)      .��6(   Ok5+      5,      �� 5-      $I��CПравила перехода считанные из файла:��6   �� 5.      -   5         ��Name$ ShowFileText��5.      $��3q�5/      $
��Wrap=�50      �� 51      $����Правила перехода корректно обработанные и преобразованные во внутренее представление КА:��6   $
��Wrap=�52      �� 53      -   5        $ CorrectRule��53      $��3q�54      $
��Wrap=�55      �� 56      $=��7Выберите стартовое состояние:��6   �� 57      -   5         ��Name$ spRadioButton��57      $ 
Horizontal=�58      $��30��59      5:      �� 5;      $ 
Horizontal=�6   .+�5<      �� $@��:Выбранное стартовое состояние: ��6   �� 5=      -   5        $ start��5=      5>      5?      �� 5@      $��1��6   $��1��6'   $��2��67   $ Vertical=�6H   .+�5A      �� $��Center=�6   RPВведите цепочку символов входного алфавита6a   �� 5B      $ 
Horizontal=�6   $��0,5,0,0q�62   .+�5C      �� $��5,0,5,0q�6   $
��auto��6-   $
��Left=�6:   $��Center=�6U   FDДля конвертации НКА в ДКА, нажмите "Оk"6�   �� 5D      -   5        $��setChainOfCommand��5D      $��5,0,0,0q�62   $��25��6C   $��25��6O   .��6z   Ok6~   5E      &   System.Windows.Controls.DataGrid  5F      -   5          Name$ workProcess��5F         AutoGenerateColumns .  $��5,5,5,0q�5H      $
��Left=�5I      	   
IsReadOnly	 .  
   Columns
 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6>   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� 5N      5O      5Q      5R      