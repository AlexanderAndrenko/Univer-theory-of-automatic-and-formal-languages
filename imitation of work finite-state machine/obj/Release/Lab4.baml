   M S B A M L   `   `   `  ���� E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator      Translator.Lab4   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     *localclr-namespace:Translator       N� Title$
  Lab4��.��� $��4q�
������� � ColumnDefinitions �� $	��400 ���  � RowDefinitions �� $��40 ��� $��* �.+��� $��0��$��2��$��Center=�.+��� $��Center=�$��Center=�$
��auto����Программа преобразователь недетерминированного кончного автомата в детерминированный�� $��1��$��0�� �� Orientation$ Vertical=�.+��� $��Center=�$��Center=�$
��Wrap=�$��3q�WUПравила вводятся в форме AaB (A -> aB), Aa (A -> a), AE (A -> E)�� $ 
Horizontal=�.+��� -   $��
ChainRules��$	��350��$
��auto�� �� TextWrapping$ WrapWithOverflow=�$��3q�.���� -   $��30��$��30��$��3q�.��Ok�� $I��CПравила перехода считанные из файла:���� -    ��Name$ ShowFileText��$��3q�$
��Wrap=��� $����Правила перехода корректно обработанные и преобразованные во внутренее представление КА:��$
��Wrap=��� -   $ CorrectRule��$��3q�$
��Wrap=��� $=��7Выберите стартовое состояние:���� -    ��Name$ spRadioButton��$ 
Horizontal=�$��30���� $ 
Horizontal=�.+��� $@��:Выбранное стартовое состояние: ���� -   $ start���� $��1��$��1��$��2��$ Vertical=�.+��� $��Center=�RPВведите цепочку символов входного алфавита�� $ 
Horizontal=�$��0,5,0,0q�.+��� $��5,0,5,0q�$
��auto��$
��Left=�$��Center=�FDДля конвертации НКА в ДКА, нажмите "Оk"�� -   $��setChainOfCommand��$��5,0,0,0q�$��25��$��25��.��Ok&   System.Windows.Controls.DataGrid  -     Name$ workProcess��   AutoGenerateColumns .  $��5,5,5,0q�$
��Left=�	   
IsReadOnly	 .  
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 