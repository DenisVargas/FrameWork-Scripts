class Color:
    def __init__(self, name: str, color: str, opacity: float):
        self.name = name
        self.color = color
        self.opacity = opacity

    def __repr__(self):
        return f"Color(name='{self.name}', color='{self.color}', opacity={self.opacity})"
    
    def generate_variable(self, include_Opacity):
        return f"--{self.name}: {self.color}{f', opacity={self.opacity}' if include_Opacity else ''};"

