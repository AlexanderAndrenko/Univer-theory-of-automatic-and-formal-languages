   M S B A M L   `   `   `  ����E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator  5      L HDataGrid2DLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibraryDataGrid2DLibrary 5          Translator.Lab3   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Translator     5      Kdg2d:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibrary     5        N� Title$
  Lab3��5
      .��5      � $��4q�6   
������� � ColumnDefinitions 5   
   �� 5      $	��480 �6   �� 5       � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $	��250 �6   �� 5      .+�5   
   �� $��0��6   $��2��6"   $��Center=�66   .+�5      �� $��Center=�6   $��Center=�65   $
��auto��6P   ��Программа построения недетерминированного конечно-автоматного распознавателя6�   �� 5      $��Center=�6   $��Center=�66   $
��auto��6Q   \Zпо заданной праволинейной автоматной грамматике6�   5      �� 5   
   $��1��6    �� Orientation$ Vertical=�6"   .+�5      �� $��1��6   $
��auto��6&   $��25��63   $	��Top=�6?   $��Center=�6W   $y��sОпределите количество терминальных и нетерминальных символов��6t   �� 5      $ 
Horizontal=�6   .+�5      �� $��1��6   $��images/terminal.png��6%   $��20��6B   $��5,0,0,0q�6N   $
��Left=�6_   �� 5      $	�� : ��6   $��16(�6'   �� 5      -   5        $��TerminalSymbol��5      $��25��60   $��25��6;   6J   �� 5       $��1��6   $��images/nonterminal.png��6%   $��20��6E   $��5,0,0,0q�6Q   $
��Left=�6b   �� 5!      $	�� : ��6   $��16(�6'   �� 5"      -   5        $��NonterminalSymbol��5"      $��25��63   $��25��6>   6M   �� 5#      -   5        $��SetSymbolOfAlphabet��5#      $��10,0,0,0q�64   $��25��6F   $��25��6R   .��6�   Ok6�   5$      �� 5%      $��1��6   $
��auto��6&   $��25��63   $	��Top=�6?   $��Center=�6W   $��0,5,0,5q�6t   $=��7Определите правила переходов:��6�   �� 5&      -   5        $��listTerminal��5&   %   $��1��6   $
��auto��69   $��25��6F   6T   �� 5'      -   5        $��listNonterminal��5'   %   $��1��6   $
��auto��6=   $��25��6J   6X   �� 5(      -   5        $��Start��5(   %   $��1��6   $
��auto��62   $��25��6?   6M   �� 5)      -   5        $
��info��5)   %   $��1��6   $
��auto��61   $��25��6>   6L   �� 5+      $ 
Horizontal=�6   .+�5,      �� -   5         ��Name$ RuleTransition��5,      $��70��62   $��25��6=   $
��Left=�6I   $��20,0,0,0q�6d   �� 5-      -	   5        $��SetRuleTransition��5-      $��10,0,0,0q�62   $��25��6D   $��25��6P   .��6|   Ok6�   �� 5.      -
   5         ��Name$ ErrorMessage��5.      $��5,0,0,0q�62   $	��340��6C   $
��Wrap=�6O   $��Center=�6c   5/      50      "  DataGrid2DLibrary.DataGrid2D  52   
   -   5        $��
dataGrid2D��52      $��2��53      $��0��6+   $��5,0,5,5q�54       �� AlternationCount$ 2��55         UseModifiedDataGridStyle . &   System.Windows.Controls.DataGrid   RowBackground
 �����	   AlternatingRowBackground	 �#ebebeb
������ $y�Hidden=�5;      
   ItemsSource2D
 57      �� *finiteMachine+5<      �� 5=   
   -   5        $��propertiesListBox��5=      $y�Hidden=�5>       �� SelectedItem$ 0��5?      .@�5A      �� .��6   finiteMachine6)   5B      �� 5D   
   $��1��6   $��1��6#   $��2��63   $ Vertical=�6D   .+�5E      �� $��Center=�6   RPВведите цепочку символов входного алфавита6a   �� 5F      $ 
Horizontal=�6   $��0,5,0,0q�62   .+�5G      �� -   5        $ chainOfcommand��5G      $��5,0,5,0q�62   $	��300��6C   $
��Left=�6O   6l   �� 5H      -   5        $��setChainOfCommand��5H      $��5,0,0,0q�62   $��25��6C   $��25��6O   .��6z   Ok6~   5I        5J      -   5          Name$ workProcess��5J         AutoGenerateColumns .  $��5,5,5,0q�6H   $
��Left=�6Z      
IsReadOnly .     Columns 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6B   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   �� 5N      5O      5Q      5R      