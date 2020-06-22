class Version
  def self.get_version
    File.read('version.txt').strip
  end
  
  def self.increment_version
    version = get_version
    regex = /(.*?)\.(\d+)$/
    if(version =~ regex)
      File.open('version.txt', 'w') do |f|
        f << "#{$1}.#{($2.to_i) + 1}"
      end
    end
    version
  end
end