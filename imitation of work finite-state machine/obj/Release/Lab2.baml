   M S B A M L   `   `   `  ���� E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator  L HDataGrid2DLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibraryDataGrid2DLibrary     Translator.Lab2   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     *localclr-namespace:Translator     Kdg2d:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibrary       N� Title$
  Lab2��.��� $��4q�
������� � ColumnDefinitions �� $	��400 ���  � RowDefinitions �� $��30 ��� $	��180 ��� .+��� $��0��$��2��$��Center=�$��186,5,184,5q�$	��414���~Добро пожаловать в симулятор детерминированного конечного автомата�� $��1�� �� Orientation$ Vertical=�.+��� $��1��$
��auto��$��25��$	��Top=�$��Center=�$T��NОпределите количество символов алфавитов:���� $ 
Horizontal=�.+��� $��1��$��images/terminal.png��$��20��$��5,0,0,0q�$
��Left=��� $	�� : ��$��16(��� -   $��TerminalSymbol��$��25��$��25���� $��1��$��images/nonterminal.png��$��20��$��5,0,0,0q�$
��Left=��� $	�� : ��$��16(��� -   $��SymbolOfState��$��25��$��25���� $��Center=�$��5,0,0,0q�=;Количество финальных состояний:�� -    ��Name$ countFinalState��$��25��$��25��$��5,0,0,0q��� -   $��SetSymbolOfAlphabet��$��10,0,0,0q�$��25��$��25��.��Ok�� $��1��$
��auto��$��25��$	��Top=�$��Center=�$��0,5,0,5q�$=��7Определите таблицу переходов:���� -   $��
listSymbol��$��1��$
��auto��$��25���� -   $��Start��$��1��$
��auto��$��25���� -   $��Final��$��1��$
��auto��$��25���� -    ��Name$ 
transition��$y�Hidden=�ZXОтсутсвие перехода обозначается пустой ячейкой"  DataGrid2DLibrary.DataGrid2D  -	   $��
dataGrid2D��$��2��$��0��$��5,0,5,5q� �� AlternationCount$ 2��   UseModifiedDataGridStyle . &   System.Windows.Controls.DataGrid   RowBackground
 �����	   AlternatingRowBackground	 �#ebebeb
������ 
   ItemsSource2D
 �� *finiteMachine+�� -
   $��propertiesListBox��$y�Hidden=� �� SelectedItem$ 0��.@��� .��finiteMachine�� $��1��$��1��$��2��$ Vertical=�.+��� $��Center=�RPВведите цепочку символов входного алфавита�� $ 
Horizontal=�$��0,5,0,0q�.+��� -   $ chainOfcommand��$��5,0,5,0q�$	��300��$
��Left=��� -   $��setChainOfCommand��$��5,0,0,0q�$��25��$��25��.��Ok  -     Name$ workProcess��   AutoGenerateColumns .  $��5,5,5,0q�$
��Left=�   
IsReadOnly .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 