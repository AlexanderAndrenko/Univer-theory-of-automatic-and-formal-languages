   M S B A M L   `   `   `  ����E  ATranslator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Translator
Translator  5      L HDataGrid2DLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullP:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibraryDataGrid2DLibrary 5          Translator.Lab2   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Translator     5      Kdg2d:clr-namespace:DataGrid2DLibrary;assembly=DataGrid2DLibrary     5        N� Title$
  Lab2��5
      .��5      � $��4q�6   
������� � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5       � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��180 �6   �� 5      .+�5   
   �� $��0��6   $��2��6!   $��Center=�65   $��186,5,184,5q�6R   $	��414��6g   �~Добро пожаловать в симулятор детерминированного конечного автомата6�   �� 5   
   $��1��6    �� Orientation$ Vertical=�6"   .+�5      �� $��1��6   $
��auto��6&   $��25��63   $	��Top=�6?   $��Center=�6W   $T��NОпределите количество символов алфавитов:��6t   �� 5      $ 
Horizontal=�6   .+�5      �� $��1��6   $��images/terminal.png��6%   $��20��6B   $��5,0,0,0q�6N   $
��Left=�6_   �� 5      $	�� : ��6   $��16(�6'   �� 5      -   5        $��TerminalSymbol��5      $��25��60   $��25��6;   6J   �� 5      $��1��6   $��images/nonterminal.png��6%   $��20��6E   $��5,0,0,0q�6Q   $
��Left=�6b   �� 5      $	�� : ��6   $��16(�6'   �� 5      -   5        $��SymbolOfState��5      $��25��6/   $��25��6:   6I   �� 5       $��Center=�6   $��5,0,0,0q�67   =;Количество финальных состояний:6i   �� 5!      -   5         ��Name$ countFinalState��5!      $��25��63   $��25��6>   $��5,0,0,0q�6J   6]   �� 5"      -   5        $��SetSymbolOfAlphabet��5"      $��10,0,0,0q�64   $��25��6F   $��25��6R   .��6�   Ok6�   5#      �� 5$      $��1��6   $
��auto��6&   $��25��63   $	��Top=�6?   $��Center=�6W   $��0,5,0,5q�6t   $=��7Определите таблицу переходов:��6�   �� 5%      -   5        $��
listSymbol��5%   %   $��1��6   $
��auto��67   $��25��6D   6R   �� 5&      -   5        $��Start��5&   %   $��1��6   $
��auto��62   $��25��6?   6M   �� 5'      -   5        $��Final��5'   %   $��1��6   $
��auto��63   $��25��6@   6N   �� 5(      -   5         ��Name$ 
transition��5(      $y�Hidden=�6,   ZXОтсутсвие перехода обозначается пустой ячейкой6p   5)      "  DataGrid2DLibrary.DataGrid2D  5+   
   -	   5        $��
dataGrid2D��5+      $��2��5,      $��0��6+   $��5,0,5,5q�5-       �� AlternationCount$ 2��5.         UseModifiedDataGridStyle . &   System.Windows.Controls.DataGrid   RowBackground
 �����	   AlternatingRowBackground	 �#ebebeb
������ 
   ItemsSource2D
 50      �� *finiteMachine+54      �� 55   
   -
   5        $��propertiesListBox��55      $y�Hidden=�56       �� SelectedItem$ 0��57      .@�59      �� .��6   finiteMachine6)   5:      �� 5<   
   $��1��6   $��1��6#   $��2��63   $ Vertical=�6D   .+�5=      �� $��Center=�6   RPВведите цепочку символов входного алфавита6a   �� 5>      $ 
Horizontal=�6   $��0,5,0,0q�62   .+�5?      �� -   5        $ chainOfcommand��5?      $��5,0,5,0q�62   $	��300��6C   $
��Left=�6O   6l   �� 5@      -   5        $��setChainOfCommand��5@      $��5,0,0,0q�62   $��25��6C   $��25��6O   .��6z   Ok6~   5A        5B      -   5          Name$ workProcess��5B         AutoGenerateColumns .  $��5,5,5,0q�6H   $
��Left=�6Z      
IsReadOnly .     Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$# Рабочий процесс��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6B   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   �� 5F      5G      5I      5J      