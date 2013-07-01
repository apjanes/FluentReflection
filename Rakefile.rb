# Directory configuration
$base_dir = File.dirname(__FILE__)
$bin_dir = File.join($base_dir, 'bin')
$rake_dir = File.join($base_dir, 'rake')
$task_dir = File.join($rake_dir, 'tasks')
$LOAD_PATH << $rake_dir
require 'version'

# Configurable properties
$solution = 'FluentReflection.sln'
$asm = {}
$asm[:company] = 'Peppermint IT Limited'
$asm[:product] = 'Fluent Reflection'
$asm[:title] = $asm[:product]
$asm[:description] = "Makes .NET reflection eminently easy through a usable, readable fluent interface."
$asm[:copyright] = "Copyright 2011 by Peppermint IT Limited"
$asm[:output] = File.join($base_dir, "src/Properties/AssemblyInfo.cs")
$asm[:namespaces] = ['System.Runtime.CompilerServices']
$asm[:custom_attributes] = {'InternalsVisibleTo'=>'FluentReflection.Tests'}
$nuspec = {} 
$nuspec[:id] = "FluentReflection"
$nuspec[:output] = "#{$nuspec[:id]}.nuspec"
$nuspec[:licenseUrl] = 'http://fluentreflection.codeplex.com/license'
$nuspec[:projectUrl] = 'http://fluentreflection.codeplex.com'


$nuget = {}
$nuget[:command] = 'C:/NuGet.exe'

# Requires
require 'rubygems'
require 'find'
unless require 'albacore'
  raise "Requires the 'albacore' gem"
end
require 'win32console'
require 'term/ansicolor'
include Term::ANSIColor

Dir.glob(File.join($task_dir, "*.task")).each do |x|
  load 'tasks/' + File.basename(x)
end



