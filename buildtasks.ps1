Import-Module .\psake.psm1

Properties {
    $root_dir = Resolve-Path ".\"
    $build_dir = "$root_dir\build"
    $src_dir = "$root_dir\src"
}

FormatTaskName (("-"*32) + " {0} " + ("-"*31))

Task Default -Depends Build

Task Build -Depends Init,Clean {
    Exec { MSBUILD "$src_dir\Wörds.Terminal\Wörds.Terminal.csproj" /t:Rebuild /p:Configuration=Release /v:Quiet /p:OutDir=$build_dir }
}

Task Clean -Depends Init {
    If (Test-Path $build_dir) {
        Write-Host "Old build directory exists. Removing ..."
		RD $build_dir -rec -force | out-null
    }
    Write-Host "Creating build artifacts directory ..."
	MKDIR $build_dir | out-null
}

Task Init {
    Write-Host "Root directory: $root_dir"
    Write-Host "Source directory: $src_dir"
    Write-Host "Build artifacts directory: $build_dir"
}