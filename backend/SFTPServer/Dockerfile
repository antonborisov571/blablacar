FROM atmoz/sftp

RUN mkdir -p /var/run/sftp && \
    chown -R root:root /var/run/sftp && \
    chmod -R 755 /var/run/sftp

COPY sftp-config.sh /entrypoint.d/

USER root