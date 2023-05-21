def LoadFileAsString(filepath: str) -> str:
    with open(filepath, "r") as f:
        content = f.read()
    return content
