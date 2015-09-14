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
        gta: {
            submodule_init: {
                command: 'submodule init',
                options: {
                    stdout: true
                }
            },
            submodule_update: {
                command: 'submodule update --recursive',
                options: {
                    stdout: true
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-msbuild');
    grunt.loadNpmTasks('grunt-nuget');
    grunt.loadNpmTasks('grunt-git-them-all');

    grunt.registerTask('default', ['gta:submodule_init', 'gta:submodule_update', 'nugetrestore:restore', 'msbuild:release', 'nugetpack:dist']);

};