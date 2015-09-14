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
        }
    });

    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');

    grunt.registerTask('default', ['nugetrestore:restore', 'msbuild:release', 'nugetpack:dist']);

};