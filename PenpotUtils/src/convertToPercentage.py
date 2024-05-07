width = int(input("Ingresa el valor de referencia"))
# heigth = int(input("Ingresa el alto de referencia"))

toConvert = int(input("ingresa el valor en pixeles que quieres convertir"))

result = (toConvert * 100) / width
print(f"El resultado es: {round(result, 2)}%")
