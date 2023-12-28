namespace Common;

public static class DotEnv
{

  public static void Load(string? filePath = null)
  { 
    
    if(string.IsNullOrEmpty(filePath)){
      var root = Directory.GetCurrentDirectory();
      filePath = Path.Combine(root, ".env");
    }

    if(!File.Exists(filePath)){
      return;
    }

    foreach(var line in File.ReadAllLines(filePath)){
      var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
      if(parts.Length != 2){
        continue;
      }
      Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
  }
}