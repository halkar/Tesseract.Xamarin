module.exports = function(grunt) {
    grunt.initConfig({
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
                dest: './'
            }
        }
    });

    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');

    grunt.registerTask('default', ['nugetrestore:restore', 'msbuild:release', 'nugetpack:dist']);

};