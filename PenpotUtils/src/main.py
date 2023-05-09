import json
from Colors import Color
from TextFonts import Typography, Font_Family
from InputHandler import LoadFile

typografy_rawData = LoadFile("../inputs/typographies.json")
Color_rawData = LoadFile("../inputs/colors.json")

def parse_typography(json_string):
    # Convertir de json a un objeto py
    parsed_data = json.loads(json_string)

    # Parsear el input
    typographies = []
    for key in parsed_data:
        typography = Typography(parsed_data[key])
        typographies.append(typography)

    return typographies

def generate_Font_Variables(typography_array : list) -> str:
    variables_str = ""
    for typos in typography_array:
        if "Docs" not in typos.name:
            variable = typos.generate_Variable(useRem=True)
            variables_str += f"{variable}\n"
    return variables_str

def extract_Font_Families(typographies: list) -> list:
    font_families = {}
    for typo in typographies:
        family = typo.fontFamily
        if family not in font_families:
            font_families[family] = True
    return list(font_families.keys())

def parse_color_json(json_str: str) -> list[Color]:
    data = json.loads(json_str)
    colors = []
    for key, value in data.items():
        name = value.get("name")
        color = value.get("color")
        opacity = value.get("opacity")
        colors.append(Color(name, color, opacity))
    return colors

def generate_color_variables(colors):
    result = ""
    for color in colors:
        result += color.generate_variable(False) + "\n"
    return result

#Typografias.
parsedTyps = parse_typography(typografy_rawData)
fontVariables = generate_Font_Variables(parsedTyps)

# Font Families
families = ""
for family in extract_Font_Families(parsedTyps):
    familyImport = Font_Family(family, "font/ttf", "url")
    families += familyImport.generate_Short() + "\n"

# Colores.
colors = parse_color_json(Color_rawData)
colorVariables = generate_color_variables(colors)

output = ":root {{\n{} \n{} \n{}\n}}".format(families, fontVariables, colorVariables)

def save_file(path: str, content: str):
    try:
        with open(path, "w") as f:
            f.write(content)
    except OSError:
        print(f"No se pudo guardar el archivo en la ruta especificada: {path}")
save_file('../Outputs/results.css', output)
