   M S B A M L   `   `   `  ���� E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator  L HDataGrid2DLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibraryDataGrid2DLibrary     Translator.Lab3   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     *localclr-namespace:Translator     Kdg2d:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibrary       N� Title$
  Lab3��.��� $��4q�
������� � ColumnDefinitions �� $	��480 ���  � RowDefinitions �� $��40 ��� $	��250 ��� .+��� $��0��$��2��$��Center=�.+��� $��Center=�$��Center=�$
��auto����Программа построения недетерминированного конечно-автоматного распознавателя�� $��Center=�$��Center=�$
��auto��\Zпо заданной праволинейной автоматной грамматике�� $��1�� �� Orientation$ Vertical=�.+��� $��1��$
��auto��$��25��$	��Top=�$��Center=�$y��sОпределите количество терминальных и нетерминальных символов���� $ 
Horizontal=�.+��� $��1��$��images/terminal.png��$��20��$��5,0,0,0q�$
��Left=��� $	�� : ��$��16(��� -   $��TerminalSymbol��$��25��$��25���� $��1��$��images/nonterminal.png��$��20��$��5,0,0,0q�$
��Left=��� $	�� : ��$��16(��� -   $��NonterminalSymbol��$��25��$��25���� -   $��SetSymbolOfAlphabet��$��10,0,0,0q�$��25��$��25��.��Ok�� $��1��$
��auto��$��25��$	��Top=�$��Center=�$��0,5,0,5q�$=��7Определите правила переходов:���� -   $��listTerminal��$��1��$
��auto��$��25���� -   $��listNonterminal��$��1��$
��auto��$��25���� -   $��Start��$��1��$
��auto��$��25���� -   $
��info��$��1��$
��auto��$��25���� $ 
Horizontal=�.+��� -    ��Name$ RuleTransition��$��70��$��25��$
��Left=�$��20,0,0,0q��� -	   $��SetRuleTransition��$��10,0,0,0q�$��25��$��25��.��Ok�� -
    ��Name$ ErrorMessage��$��5,0,0,0q�$	��340��$
��Wrap=�$��Center=�"  DataGrid2DLibrary.DataGrid2D  -   $��
dataGrid2D��$��2��$��0��$��5,0,5,5q� �� AlternationCount$ 2��   UseModifiedDataGridStyle . &   System.Windows.Controls.DataGrid   RowBackground
 �����	   AlternatingRowBackground	 �#ebebeb
������ $y�Hidden=�
   ItemsSource2D
 �� *finiteMachine+�� -   $��propertiesListBox��$y�Hidden=� �� SelectedItem$ 0��.@��� .��finiteMachine�� $��1��$��1��$��2��$ Vertical=�.+��� $��Center=�RPВведите цепочку символов входного алфавита�� $ 
Horizontal=�$��0,5,0,0q�.+��� -   $ chainOfcommand��$��5,0,5,0q�$	��300��$
��Left=��� -   $��setChainOfCommand��$��5,0,0,0q�$��25��$��25��.��Ok  -     Name$ workProcess��   AutoGenerateColumns .  $��5,5,5,0q�$
��Left=�   
IsReadOnly .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� 