   M S B A M L   `   `   `  ����R  NSharpGL.WPF, Version=2.3.0.1, Culture=neutral, PublicKeyToken=27fc851303210b27>.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPFSharpGL.WPF  5   	    AssimpSample   AssimpSample.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   BsharpGL.clr-namespace:SharpGL.WPF;assembly=SharpGL.WPF     5   	   -   5          � Title$  SharpGL WPF Application��5   	   $	��500��6)   $	��600��66   .��5      � .+�5   
   `� $	��Top=�6   .�5      c� $��Center=�6   $��Center=�63   $��45��6O   $��
0,0,-137,0q�6[   $	��592��6o   .@�5
      �� $��Faktor skaliranja:��6   $��Center=�65   $	��100��6W   6e   �� 5      -   5        $��
skaliranje��5      $��24��6-    �� TextWrapping$
 Wrap=�69   $��1q�6M   
���   �$��Center=�6u   $��30��6�   $��0,0,0,8q�6�   $��Bottom=�6�   �� 5      $��Boja izvora:��6   $��Center=�6/   $��70��6Q   6^   �� 5      $��R��6   $��Center=�6$   $��20��6F   6S   �� 5      -   5        $��bojaR��5      $��25��6(   $
 Wrap=�64   $��1q�6H   
���   �$��Center=�6p   $��30��6�   6�   �� 5      $��G��6   $��Center=�6$   $��20��6F   6S   �� 5      -   5        $��bojaG��5      $��25��6(   $
 Wrap=�64   $��1q�6H   
���   �$��Center=�6p   $��30��6�   6�   �� 5      $��B��6   $��Center=�6$   $��20��6F   6S   �� 5      -   5        $��bojaB��5      $��25��6(   $
 Wrap=�64   $��1q�6H   
���   �$��Center=�6p   $��30��6�   6�   �� 5      $��Pomeranje izvora:��6   $��Center=�64   $	��102��6V   6e   ]� 5      $��40��6   $	��100��6(   .+�5      �� -   5        $��Pomeraj��5      $��25��6.   $
 Wrap=�6:   $��1q�6N   
���   �$��Center=�6v   $��30��6�   6�   �� 5      -   5        $��25��5      $��25��6(   $��Center=�64   $��10,0,0,0q�6k   .��6}   +6�   �� 5      -   5        $��25��5      $��25��6(   $��Center=�64   .��6n   -6q   5      5      5         SharpGL.WPF.OpenGLControl  5!   
   -	   5        $��openGLControl��5"         DrawFPS .    RenderContextType$	 FBO=�5$         	FrameRate$ 10Q�64   $��0,45,0,0q�6C   5&      5'      