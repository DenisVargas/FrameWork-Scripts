class Typography:
    def __init__(self, data) -> None:
        self.textTransform = data["textTransform"]
        self.fontFamily = data["fontFamily"]
        self.fontStyle = data["fontStyle"]
        self.fontWeight = data["fontWeight"]
        self.lineHeight = data["lineHeight"]
        self.path = data["path"]
        self.letterSpacing = data["letterSpacing"]
        self.name = data["name"]
        self.fontVariantId = data["fontVariantId"]
        self.fontSize = data["fontSize"]
        self.fontId = data["fontId"]
        self.fontSizeBase = 16
        self.fontSizeRem = float(self.fontSize) / self.fontSizeBase

    def change_Base_Font_Size(self, baseSize):
        self.fontSizeBase = baseSize
        self.fontSizeRem = float(self.fontSize) / self.fontSizeBase

    def generate_Short(self):
        return f'font: {self.fontStyle} {self.fontWeight} {self.fontSize}px {self.fontFamily};'

    def generate_Variable(self, useRem):
        return f'--{self.name}: {self.fontStyle} {self.fontWeight} {str(self.fontSizeRem) + "rem" if useRem else self.fontSize + "px"} {self.fontFamily};'

class Font_Family:
    def __init__(self, font_family, MIMEType, src):
        self.font_family = font_family
        self.mime = MIMEType
        if MIMEType == "font/ttf":
            self.format = "TrueType"
            self.extention = ".ttf"
        if MIMEType == "font/otf":
            self.format = "OpenType"
            self.extention = ".otf"
        if MIMEType == "font/woff":
            self.format = "Web Open Font Format"
            self.extention = ".woff"
        if MIMEType == "font/woff2":
            self.format = "Web Open Font Format 2"
            self.extention = ".woff2"
        if src == "local":
            self.src = f"local(\"{self.font_family}\")"
        if src == "url":
            self.src = f"url(\"path/to/{self.font_family}{self.extention}\")"

    def generate_Short(self):
        return f"@font-face {{\n  font-family: \"{self.font_family}\";\n  src: {self.src} format(\"{self.format}\");\n}}"