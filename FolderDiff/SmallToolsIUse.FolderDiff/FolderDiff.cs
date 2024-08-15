namespace SmallToolsIUse
{
    public class FolderDiff(string sourcePath, string targetPath)
    {
        private readonly string sourcePath = sourcePath;
        private readonly string targetPath = targetPath;

        public FolderDiffResult Compare()
        {
            IEnumerable<string> sourceFiles = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories).Select(f => f.Replace(sourcePath, string.Empty));
            IEnumerable<string> targetFiles = Directory.EnumerateFiles(targetPath, "*.*", SearchOption.AllDirectories).Select(f => f.Replace(targetPath, string.Empty));

            List<string> onlyInSource = sourceFiles.Except(targetFiles).ToList();
            List<string> onlyInTarget = targetFiles.Except(sourceFiles).ToList();
            List<string> inBothButDifferent = sourceFiles.Intersect(targetFiles)
                .Where(f => new FileInfo($"{sourcePath}\\{f}").Length != new FileInfo($"{targetPath}\\{f}").Length)
                .ToList();
            
            IEnumerable<string> sourceFolders = Directory.EnumerateDirectories(sourcePath, "*", SearchOption.AllDirectories).Select(f => f.Replace(sourcePath, string.Empty));
            IEnumerable<string> targetFolders = Directory.EnumerateDirectories(targetPath, "*", SearchOption.AllDirectories).Select(f => f.Replace(sourcePath, string.Empty));
            List<string> foldersOnlyInSource = sourceFolders.Except(targetFolders).ToList();
            List<string> foldersOnlyInTarget = targetFolders.Except(sourceFolders).ToList();


            return new FolderDiffResult(onlyInSource, onlyInTarget, inBothButDifferent, foldersOnlyInSource, foldersOnlyInTarget);
        }
    }

    public class FolderDiffResult
    {
        public List<string> FilesOnlyInSource { get; }
        public List<string> FilesOnlyInTarget { get; }
        public List<string> FilesInBothButDifferent { get; }
        public List<string> FoldersOnlyInSource { get; }
        public List<string> FoldersOnlyInTarget { get; }

        public FolderDiffResult(List<string> filesOnlyInSource, 
            List<string> filesOnlyInTarget, 
            List<string> filesInBothButDifferent,
            List<string> foldersOnlyInSource,
            List<string> foldersOnlyInTarget)
        {
            FilesOnlyInSource = filesOnlyInSource;
            FilesOnlyInTarget = filesOnlyInTarget;
            FilesInBothButDifferent = filesInBothButDifferent;
            FoldersOnlyInSource = foldersOnlyInSource;
            FoldersOnlyInTarget = foldersOnlyInTarget;
        }

        public bool FoldersAreIdentical()
        {
            return FilesOnlyInSource.Count == 0 &&
                FilesOnlyInTarget.Count == 0 &&
                FilesInBothButDifferent.Count == 0 &&
                FoldersOnlyInSource.Count == 0 &&
                FoldersOnlyInTarget.Count == 0;
        }
    }
}
