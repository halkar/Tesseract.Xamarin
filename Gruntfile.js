module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        msbuild: {
            release: {
                src: 'Tesseract.Xamarin.sln',
                options: {
                    projectConfiguration: 'Release'
                }
            }
        },
        nugetrestore: {
            restore: {
                src: 'Tesseract.Xamarin.sln',
                dest: 'packages/'
            }
        },
        nugetpack: {
            dist: {
                src: 'Xamarin.Tesseract.nuspec',
                dest: './',
                options: {
                    version: '<%= pkg.version %>'
                }
            }
        },
        assemblyinfo: {
            options: {
                // Can be solutions, projects or individual assembly info files 
                files: ['SharedAssemblyInfo.cs'],
                // Standard assembly info 
                info: {
                    version: '<%= pkg.version %>.0',
                    fileVersion: '<%= pkg.version %>.0'
                }
    }
}
    });

    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');
    grunt.loadNpmTasks('grunt-dotnet-assembly-info');

    grunt.registerTask('default', ['nugetrestore:restore', 'assemblyinfo', 'msbuild:release', 'nugetpack:dist']);

};