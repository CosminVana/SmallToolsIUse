# SmallToolsIUse
A Repository with small tools I use during my work

## FolderDiff
Compares the structure of 2 directories and prints the following differences:
- Files existing only in source
- Files existing only in target
- Files existing in both directories, but of different size
- Folders existing only in source
- Folders existing only in destination

Please note that some files may have the same size, but different content. They will not be returned as differences (e.g. Bitmap images will have fixed file size based on image size, regardless of it's content, Text files will have the same size if they have the same number of characters). No further enhancements are planned to address file content differences, but it can be easily implemented in FolderDiff class.

### Usage
<code>SmallToolsIUse.FolderDiff.exe "C:\Some Folder" "C:\Some other folder"</code>

### Release
[Go to release page](https://github.com/CosminVana/SmallToolsIUse/releases/tag/FolderDiff)


