   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5          WpfApp2.MainPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfApp2     5      $	��450��5      $	��450��5	      $	��500��5
      $	��500��5      $	��450��5        N� Title$&   Запись к префекту��5      .��5      � $��10q�5   	   
�������.+�5   
   �� $+��%Выберите тип приема:��5      $��Center=�5      $	��Top=�5      �� 5   
   -   5         ��Name$ AppointmentTypeComboBox��5      $��Center=�5      $	��Top=�5      $��0,30,0,0q�5      $	��100��5      .@�5      �� $��Личный прием��5      �� 5      $+��%Онлайн-консультация��5       5!      �� 5$   
   $ ��Выберите дату:��5%      $��Center=�5&      $	��Top=�5'      $��0,70,0,0q�5(      (  "System.Windows.Controls.DatePicker  5)   
   -   5          Name$ AppointmentDatePicker��5*      $��Center=�5+      $	��Top=�5,      $��	0,100,0,0q�5-      �� 5/   
   $$��Введите Фамилию:��50      $��Center=�51      $	��Top=�52      $��	0,140,0,0q�53      �� 54   
   -   5         ��Name$ UserSurNameTextBox��55      $��Center=�56      $	��Top=�57      $��	0,170,0,0q�58      $	��150��59      �� 5;   
   $��Введите Имя:��5<      $��Center=�5=      $	��Top=�5>      $��	0,190,0,0q�5?      �� 5@   
   -   5        $ UserFurstNameTextBox��5A      $��Center=�5B      $	��Top=�5C      $��	0,220,0,0q�5D      $	��150��5E      �� 5H   
   $%��Введите Отчество��5I      $��Center=�5J      $	��Top=�5K      $��
0 240 0 0 q�5L      �� 5O   
   -   5        $ UserLastNameTextBox��5P      $��Center=�5Q      $	��Top=�5R      $��	0 270 0 0q�5S      $	��150��5T      �� 5V   
   -   5        $��Записаться��5W      $��Center=�5X      $	��Top=�5Y      $��	0,300,0,0q�5Z      5[      �� 5\   
   -   5         ��Name$ ResultTextBlock��5]      $��Center=�5^      $	��Top=�5_      $��	0,250,0,0q�5`      
��� � �5a      5b      5c      