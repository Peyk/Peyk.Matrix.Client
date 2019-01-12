const $ = require('shelljs')
const path = require('path')
require('../logging')

$.config.fatal = true
const root = path.resolve(`${__dirname}/../..`)
const deploy_settings = require('./../deploy/deploy_settings')
const docker_deployment = require('./../deploy/deploy_docker_registry')

try {
    console.info(`BUILDING DOCKER IMAGES`)

    $.cd(root)

    console.debug('building the solution with "peyk-matrix-client:solution-debug" tag')
    $.exec(`
        docker                                  \
        build                                   \
        --tag peyk-matrix-client:solution-debug \
        --build-arg configuration=Debug         \
        .
    `)

    console.debug('building the solution with "peyk-matrix-client:solution-release" tag')
    $.exec(`
        docker                                      \
        build                                       \
        --tag peyk-matrix-client:solution-release   \
        --build-arg configuration=Release           \
        .
    `)


    console.debug('reading Docker deployment options')
    const docker_options = deploy_settings.get_docker_settings()
    if (docker_options) {
        console.debug('pushing images to the Docker hub')

        docker_deployment.deploy(
            'peyk-matrix-client:solution-debug',
            'peyk/matrix-client:solution-debug',
            docker_options.user,
            docker_options.pass
        )

        docker_deployment.deploy(
            'peyk-matrix-client:solution-release',
            'peyk/matrix-client:solution-release',
            docker_options.user,
            docker_options.pass
        )
    } else {
        console.warn('Docker deployment options not found. skipping Docker image push...')
    }
} catch (e) {
    console.error(`❎ AN UNEXPECTED ERROR OCURRED`)
    console.error(e)
    process.exit(1)
}

console.info(`✅ BUILD SUCCEEDED`)