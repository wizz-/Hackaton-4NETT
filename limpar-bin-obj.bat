@echo off
chcp 65001 >nul
echo Excluindo todas as pastas 'bin' e 'obj' a partir do diretório atual...
echo.

:: Procura e deleta todas as pastas 'bin' e 'obj' dentro da estrutura de diretórios
for /d /r . %%d in (bin,obj) do (
    if exist "%%d" (
        echo Deletando: %%d
        rd /s /q "%%d"
    )
)

echo.
echo Limpeza concluída!
pause
