## [2.0.3]

### Update

- Fixed bug that prevents to create event string when string event scriptable object not created yet
- Fixed bug that create wrong object when try to create dialogue container

## [2.0.2]

### Update

- String events instead of string now depence from scriptable object that holds in resources file

## [2.0.1]

### Update

- Add ability to save and load names to/from CSV

## [2.0.0]

### Update

- Refactor code, now insted of reference type object use value types
- Add new node types
- Add colors to different nodes

### Removed

- Names no more saves in CSV file for translation (Will fix in next version) 

## [1.0.2]

### Update

- Update meta files

## [1.0.1]

### Update

- Unity minimum version for graph view experemental system

### Removed

- Example scene creates erros and can't be open through downloads, so we remove it from package

## [1.0.0]

### Added

- Add base editor and runtime functional such as:
- Creating dialogue scriptable object files
- Dialogue editor tool, that helps create and editing dialogue graphs
- Collecting, save and load node data
- 6 different types of nodes: start, dialogue, branch, event and end
- Ability to save and load CSV files with dialogue text to simplify translation
- Example scene with functional demo
