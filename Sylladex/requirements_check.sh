# Prints project size (pure C# code) in KB
find . -name "*.cs" -not -path "*/Debug/*" -exec du -b {} + | awk '{total += $1} END {print total/1024 " KB"}'